using Entities.Tickets;

namespace Application.Gateways
{
    public interface ITicketGateway
    {
        Task Save(Ticket ticket);
        Task<Ticket?> GetByOrderId(Guid orderId);
        Task Update(Ticket ticket);
    }
}
