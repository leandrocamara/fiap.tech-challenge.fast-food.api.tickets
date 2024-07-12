using External.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace API.HealthChecks;

public static class Extensions
{
    public static IServiceCollection AddCustomHealthChecks(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddCheck<DbHealthCheck>(
                name: "db_health_check",
                tags: new List<string> { "database", "healthcheck" })
            .AddSqsHealthCheck(configuration);

        return services;
    }

    public static void UseCustomHealthChecks(this IApplicationBuilder builder)
    {
        builder
            .UseHealthChecks("/heartbeat", new HealthCheckOptions
            {
                Predicate = _ => false,
                ResponseWriter = (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(report, Formatting.Indented));
                }
            })
            .UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("healthcheck"),
                ResponseWriter = HealthCheckResponseWriter
            });
    }

    private static Task HealthCheckResponseWriter(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";

        var serializerSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        var response = JsonConvert.SerializeObject(report, Formatting.Indented, serializerSettings);

        return context.Response.WriteAsync(response);
    }
}