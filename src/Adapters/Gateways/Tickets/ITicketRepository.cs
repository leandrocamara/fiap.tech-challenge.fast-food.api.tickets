using Entities.Tickets;

namespace Adapters.Gateways.Tickets;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<Ticket?> GetTicketByOrderIdAsync(Guid orderId);
}