using Application.Gateways;
using Entities.SeedWork;

namespace Application.UseCases.Tickets;

public interface IUpdateStatusUseCase : IUseCase<UpdateStatusRequest, UpdateStatusResponse>;

public sealed class UpdateStatusUseCase(
    ITicketGateway ticketGateway,
    IOrderGateway orderGateway) : IUpdateStatusUseCase
{
    public async Task<UpdateStatusResponse> Execute(UpdateStatusRequest request)
    {
        try
        {
            var ticket = await ticketGateway.GetByOrderId(request.OrderId);

            if (ticket == null)
                throw new ApplicationException("Ticket not found");

            ticket.UpdateStatus();
            await ticketGateway.Update(ticket);

            await orderGateway.UpdateStatusOrder(ticket.OrderId, ticket.Status);

            return new UpdateStatusResponse(ticket.Id, ticket.OrderId, ticket.Status);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover ticket. Error: {e.Message}", e);
        }
    }
}

public record UpdateStatusRequest(Guid OrderId);

public record UpdateStatusResponse(Guid Id, Guid OrderId, string Status);