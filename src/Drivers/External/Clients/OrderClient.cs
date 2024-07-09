using Adapters.Gateways.Orders;

namespace External.Clients;

public class OrderClient : IOrderClient
{
    public Task UpdateStatusOrder(Guid orderId, int orderStatus)
    {
        // TODO: HTTP or Messaging
        throw new NotImplementedException();
    }
}