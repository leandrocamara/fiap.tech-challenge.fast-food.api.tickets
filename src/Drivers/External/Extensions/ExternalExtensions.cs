using Adapters.Gateways.Orders;
using External.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace External.Extensions;

public static class ExternalExtensions
{
    public static IServiceCollection AddExternalDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Configure AWS DynamoDB

        services.AddScoped<IOrderClient, OrderClient>();

        return services;
    }

    public static void CreateDatabase(this IApplicationBuilder _, IConfiguration configuration)
    {
        // TODO: Create on AWS DynamoDB
    }
}