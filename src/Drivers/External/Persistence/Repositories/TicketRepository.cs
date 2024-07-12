using Adapters.Gateways.Tickets;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Entities.Tickets;

namespace External.Persistence.Repositories;

public class TicketRepository(IAmazonDynamoDB dynamoDbClient) : BaseRepository<Ticket>(dynamoDbClient), ITicketRepository
{
    
}