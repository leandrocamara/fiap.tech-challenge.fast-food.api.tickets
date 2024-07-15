using Adapters.Gateways.Tickets;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Entities.Tickets;

namespace External.Persistence.Repositories;

public class TicketRepository(IDynamoDBContext context) : BaseRepository<Ticket>(context), ITicketRepository
{
    
}