using Adapters.Controllers;
using Adapters.Gateways.Orders;
using Adapters.Gateways.Tickets;
using Application.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Extensions;

public static class AdaptersExtensions
{
    public static IServiceCollection AddAdaptersDependencies(this IServiceCollection services)
    {
        services.AddScoped<ITicketController, TicketController>();

        services.AddScoped<ITicketGateway, TicketGateway>();
        services.AddScoped<IOrderGateway, OrderGateway>();

        return services;
    }
}