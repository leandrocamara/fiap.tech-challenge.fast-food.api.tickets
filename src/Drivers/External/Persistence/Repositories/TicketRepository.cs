using Adapters.Gateways.Tickets;
using Entities.Tickets;

namespace External.Persistence.Repositories;

public class TicketRepository : ITicketRepository
{
    public Task Add(Ticket entity) => throw new NotImplementedException();

    public Task Update(Ticket entity) => throw new NotImplementedException();

    public Task Delete(Ticket entity) => throw new NotImplementedException();

    public Task<Ticket?> GetById(Guid id) => throw new NotImplementedException();
}