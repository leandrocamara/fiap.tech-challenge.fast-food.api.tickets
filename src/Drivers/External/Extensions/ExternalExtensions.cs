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
using Amazon.Extensions.NETCore.Setup;



namespace External.Extensions;

public static class ExternalExtensions
{
    public static IServiceCollection AddExternalDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {  

        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IOrderClient, OrderClient>();

        var amazonSettings = GetAmazonSettings(configuration);
        SetupAmazonSqs(services, amazonSettings);
        SetupAmazonDynamoDb(services, amazonSettings);
        

        return services;
    }

    public static void CreateDatabase(this IApplicationBuilder _, IServiceProvider serviceProvider)
    {   
        serviceProvider.GetService<DatabaseContextInitializer>();
    }

    private static void SetupAmazonDynamoDb(IServiceCollection services, AmazonSettings settings)
    {
        var credentials = new SessionAWSCredentials(settings.AccessKey, settings.SecretKey, settings.SessionToken);
        var region = RegionEndpoint.GetBySystemName(settings.Region);

        var awsOptions = new AWSOptions
        {
            Credentials = credentials,
            Region = region
        };

        services.AddDefaultAWSOptions(awsOptions);

        services.AddSingleton<IDynamoDBContext,DynamoDBContext>();
        var client = new AmazonDynamoDBClient(credentials, region);
        
        services.AddSingleton<IAmazonDynamoDB>(client);
        services.AddSingleton<IDynamoDbDatabaseContext, TicketDynamoDbDatabaseContext>();
        services.AddSingleton<DatabaseContextInitializer>();
        services.AddSingleton(new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 });
    }

    private static void SetupAmazonSqs(IServiceCollection services, AmazonSettings settings)
    { 

        services.AddSingleton<IAmazonSQS>(_ => new AmazonSQSClient(
            new SessionAWSCredentials(settings.AccessKey, settings.SecretKey, settings.SessionToken),
            new AmazonSQSConfig { RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region) }));

        services.AddHostedService<TicketCreatedConsumer>();
    }

    public static IHealthChecksBuilder AddSqsHealthCheck(
        this IHealthChecksBuilder builder, IConfiguration configuration)
    {
        var settings = GetAmazonSettings(configuration);

        return builder.AddSqs(options =>
        {
            options.Credentials = new SessionAWSCredentials(
                settings.AccessKey,
                settings.SecretKey,
                settings.SessionToken);
            options.RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region);
        }, name: "sqs_health_check", tags: new[] { "sqs", "healthcheck" });
    }

    private static AmazonSettings GetAmazonSettings(IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(AmazonSettings)).Get<AmazonSettings>();

        if (settings is null)
            throw new ArgumentException($"{nameof(AmazonSettings)} not found.");

        return settings;
    }
}