using Entities.Tickets;

namespace Application.Gateways;

public interface IOrderGateway
{
    Task UpdateStatusOrder(Guid orderId, TicketStatus status);
}