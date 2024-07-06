using Entities.Tickets;

namespace Application.Gateways
{
    public interface ITicketGateway
    {
        Task Save(Ticket ticket);
        Task<Ticket?> GetById(Guid id);
        Task Update(Ticket ticket);
    }
}
