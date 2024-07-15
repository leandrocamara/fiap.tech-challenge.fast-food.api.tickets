using Adapters.Gateways.Tickets;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Entities.Tickets;
using External.Persistence.Tables;

namespace External.Persistence.Repositories;

public class TicketRepository(IDynamoDBContext context) : BaseRepository<Ticket>(context), ITicketRepository
{
    public async Task<Ticket?> GetTicketByOrderIdAsync(Guid orderId)
    {
        var queryFilter = new List<ScanCondition>
            {
                new(nameof(Ticket.OrderId), ScanOperator.Equal, orderId)
            };
        
        var results = await context.ScanAsync<Ticket>(queryFilter).GetRemainingAsync();
        return results.Any() ? results[0] : null;
    }
}