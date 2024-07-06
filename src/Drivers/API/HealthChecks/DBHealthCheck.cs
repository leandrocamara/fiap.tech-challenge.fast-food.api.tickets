using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.HealthChecks;

public class DbHealthCheck(IConfiguration configuration) : IHealthCheck
{
    private readonly string? _connectionString = configuration.GetConnectionString("Default");

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // TODO: AWS DynamoDB HealthCheck

            return Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception e)
        {
            return Task.FromResult(HealthCheckResult.Degraded(exception: e));
        }
    }
}