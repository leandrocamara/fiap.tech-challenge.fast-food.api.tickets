using Amazon.DynamoDBv2;
using External.Persistence.Tables;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace External.HealthChecks;

public class DbHealthCheck : IHealthCheck
{
    
    private readonly IAmazonDynamoDB _dynamoDbClient;

    public DbHealthCheck(IAmazonDynamoDB dynamoDbClient)
    {
        _dynamoDbClient = dynamoDbClient ?? throw new ArgumentNullException(nameof(dynamoDbClient));
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Faz uma operação simples para verificar a conectividade com o DynamoDB
            var describeTableRequest = new Amazon.DynamoDBv2.Model.DescribeTableRequest
            {
                TableName = TicketDefinitions.TABLE_NAME
            };

            await _dynamoDbClient.DescribeTableAsync(describeTableRequest, cancellationToken);

            return HealthCheckResult.Healthy();
            
        }
        catch (Exception e)
        {
            return HealthCheckResult.Degraded(exception: e);
        }
    }
}