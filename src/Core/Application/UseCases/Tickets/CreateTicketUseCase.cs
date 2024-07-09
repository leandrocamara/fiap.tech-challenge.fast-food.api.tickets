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
            var ticket = new Ticket(request.OrderId, GetTicketItems(request));

            await ticketGateway.Save(ticket);

            return new CreateTicketResponse(ticket);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to save ticket. Error: {e.Message}", e);
        }
    }

    private IEnumerable<TicketItem> GetTicketItems(CreateTicketRequest request)
    {
        return request.TicketItems.Select(item => new TicketItem(
            new Product(item.Product.Id, item.Product.Name, item.Product.Category, item.Product.Description),
            item.Quantity));
    }
}

public record CreateTicketRequest(Guid OrderId, IEnumerable<TicketItemRequest> TicketItems);

public record TicketItemRequest(ProductRequest Product, short Quantity);

public record ProductRequest(Guid Id, string Name, string Category, string Description);

public record CreateTicketResponse(Ticket Ticket);