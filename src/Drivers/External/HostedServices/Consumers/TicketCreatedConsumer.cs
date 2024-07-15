using Adapters.Controllers;
using Amazon.SQS;
using Application.UseCases.Tickets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace External.HostedServices.Consumers;

public sealed class TicketCreatedConsumer(
    IServiceProvider serviceProvider,
    IAmazonSQS sqsClient,
    ILogger<SqsConsumerHostedService<CreateTicketRequest>> logger)
    : SqsConsumerHostedService<CreateTicketRequest>(serviceProvider, sqsClient, logger)
{
    protected override string QueueName() => "ticket-created";

    protected override Task Process(IServiceScope scope, CreateTicketRequest request)
    {
        var ticketController = scope.ServiceProvider.GetRequiredService<ITicketController>();
        return ticketController.CreateTicket(request);
    }
}