using Application.Gateways;
using Entities.Tickets;

namespace Adapters.Gateways.Orders;

public class OrderGateway(IOrderClient orderClient) : IOrderGateway
{
    public Task UpdateStatusOrder(Guid orderId, TicketStatus status)
    {
        var orderStatus = (int) status; // TODO: Convert Status
        return orderClient.UpdateStatusOrder(orderId, orderStatus);
    }
}