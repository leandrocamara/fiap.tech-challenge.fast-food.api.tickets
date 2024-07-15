namespace Adapters.Gateways.Orders;

public interface IOrderClient
{
    Task UpdateStatusOrder(Guid orderId, string ticketStatus);
}