using Adapters.Gateways.Orders;
using Adapters.Gateways.Tickets;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using External.Clients;
using External.Persistence;
using External.HostedServices.Consumers;
using External.Persistence.Repositories;
using External.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


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
        services.AddSingleton(new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 });

        services.AddScoped<ITicketRepository, TicketRepository>();

        services.AddScoped<IOrderClient, OrderClient>();

        SetupAmazonSqs(services, configuration);

        return services;
    }

    public static void CreateDatabase(this IApplicationBuilder _, IServiceProvider serviceProvider)
    {   
        serviceProvider.GetService<DatabaseContextInitializer>();
    }

    private static void SetupAmazonSqs(IServiceCollection services, IConfiguration configuration)
    {
        var settings = GetAmazonSqsSettings(configuration);

        services.AddSingleton<IAmazonSQS>(_ => new AmazonSQSClient(
            new SessionAWSCredentials(settings.AccessKey, settings.SecretKey, settings.SessionToken),
            new AmazonSQSConfig { RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region) }));

        services.AddHostedService<TicketCreatedConsumer>();
    }

    public static IHealthChecksBuilder AddSqsHealthCheck(
        this IHealthChecksBuilder builder, IConfiguration configuration)
    {
        var settings = GetAmazonSqsSettings(configuration);

        return builder.AddSqs(options =>
        {
            options.Credentials = new SessionAWSCredentials(
                settings.AccessKey,
                settings.SecretKey,
                settings.SessionToken);
            options.RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region);
        }, name: "sqs_health_check", tags: new[] { "sqs", "healthcheck" });
    }

    private static AmazonSqsSettings GetAmazonSqsSettings(IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(AmazonSqsSettings)).Get<AmazonSqsSettings>();

        if (settings is null)
            throw new ArgumentException($"{nameof(AmazonSqsSettings)} not found.");

        return settings;
    }
}