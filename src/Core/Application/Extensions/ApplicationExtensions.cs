using Application.UseCases.Tickets;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICreateTicketUseCase, CreateTicketUseCase>();
        services.AddScoped<IUpdateStatusUseCase, UpdateStatusUseCase>();

        return services;
    }
}