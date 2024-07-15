using Amazon.DynamoDBv2.DataModel;

namespace Entities.SeedWork;

public abstract class Entity
{
    [DynamoDBHashKey("pk")]
    public Guid Id { get; protected set; }
}