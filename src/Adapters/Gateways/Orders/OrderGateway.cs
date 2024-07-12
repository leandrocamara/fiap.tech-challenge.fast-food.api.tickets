using Application.Gateways;
using Entities.Tickets;

namespace Adapters.Gateways.Orders;

public class OrderGateway(IOrderClient orderClient) : IOrderGateway
{
    public Task UpdateStatusOrder(Guid orderId, TicketStatus status) => 
        orderClient.UpdateStatusOrder(orderId, status);
}