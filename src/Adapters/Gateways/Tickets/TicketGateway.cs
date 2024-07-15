using Application.Gateways;
using Entities.Tickets;

namespace Adapters.Gateways.Tickets;

public class TicketGateway(ITicketRepository repository) : ITicketGateway
{
    public Task Save(Ticket ticket) => repository.Add(ticket);

    public Task<Ticket?> GetById(Guid orderId) => repository.GetTicketByOrderIdAsync(orderId);

    public Task Update(Ticket ticket) => repository.Update(ticket);
}