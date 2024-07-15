using Adapters.Gateways.Orders;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.Clients;

public class OrderClient(IAmazonSQS sqsClient, ILogger<OrderClient> logger) : IOrderClient
{
    private const string QueueName = "ticket-updated";

    public Task UpdateStatusOrder(Guid orderId, string ticketStatus)
    {
        var message = JsonConvert.SerializeObject(new TicketUpdated(orderId, ticketStatus));
        logger.LogInformation("Publishing message: {Text}", message);

        return sqsClient.SendMessageAsync(new SendMessageRequest { QueueUrl = QueueName, MessageBody = message });
    }
}

public record TicketUpdated(Guid OrderId, string TicketStatus);