using Application.Gateways;
using Entities.SeedWork;

namespace Application.UseCases.Tickets;

public interface IUpdateStatusUseCase : IUseCase<UpdateStatusRequest, bool>;

public sealed class UpdateStatusUseCase(
    ITicketGateway ticketGateway,
    IOrderGateway orderGateway) : IUpdateStatusUseCase
{
    public async Task<bool> Execute(UpdateStatusRequest request)
    {
        try
        {
            var ticket = await ticketGateway.GetById(request.OrderId);

            if (ticket == null)
                throw new ApplicationException("Ticket not found");

            ticket.UpdateStatus();
            await ticketGateway.Update(ticket);

            await orderGateway.UpdateStatusOrder(ticket.OrderId, ticket.Status);

            return true;
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover ticket. Error: {e.Message}", e);
        }
    }
}

public record UpdateStatusRequest(Guid OrderId);