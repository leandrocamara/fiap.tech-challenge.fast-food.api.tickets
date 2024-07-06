using Application.Gateways;
using Entities.SeedWork;
using Entities.Tickets;

namespace Application.UseCases.Tickets;

public interface ICreateTicketUseCase : IUseCase<CreateTicketRequest, CreateTicketResponse>;

public sealed class CreateTicketUseCase(ITicketGateway ticketGateway) : ICreateTicketUseCase
{
    public async Task<CreateTicketResponse> Execute(CreateTicketRequest request)
    {
        try
        {
            var ticket = new Ticket(request.OrderId, TicketStatus.Received());

            await ticketGateway.Save(ticket);

            return new CreateTicketResponse(ticket);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover product. Error: {e.Message}", e);
        }
    }
}

public record CreateTicketRequest(Guid OrderId); // TODO: incluir itens da comanda (produtos/itens do pedido) 

public record CreateTicketResponse(Ticket Ticket);