using Adapters.Gateways;
using Adapters.Gateways.Orders;
using Adapters.Gateways.Tickets;
using Amazon.DynamoDBv2;
using External.Clients;
using External.HealthChecks;
using External.Persistence;
using External.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;


namespace External.Extensions;

public static class ExternalExtensions
{
    public static IServiceCollection AddExternalDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        var awsOptions = configuration.GetAWSOptions();        
        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonDynamoDB>(awsOptions);
        services.AddSingleton<IDynamoDbDatabaseContext, TicketDynamoDbDatabaseContext>();
        services.AddSingleton<DatabaseContextInitializer>();

        services.AddScoped<ITicketRepository, TicketRepository>();

        services.AddScoped<IOrderClient, OrderClient>();

        return services;
    }

    public static void CreateDatabase(this IApplicationBuilder _, IServiceProvider serviceProvider)
    {   
        serviceProvider.GetService<DatabaseContextInitializer>();
    }
    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<DbHealthCheck>(
                name: "db_health_check",
                tags: new List<string> { "database", "healthcheck" });

        return services;
    }
}