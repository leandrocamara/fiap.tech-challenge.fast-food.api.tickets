using Entities.SeedWork;

namespace Entities.Tickets;

public sealed class Ticket : Entity, IAggregatedRoot
{
    public Guid OrderId { get; private set; }
    public TicketStatus Status { get; private set; }

    // TODO: CreatedAt, UpdatedAt

    public Ticket(Guid orderId, TicketStatus status)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Status = status;
    }

    public void UpdateStatus()
    {
        if (StatusSequence.TryGetValue(Status, out var nextStatus))
            Status = nextStatus;
    }

    private static readonly Dictionary<TicketStatus, TicketStatus> StatusSequence = new()
    {
        { TicketStatus.Received(), TicketStatus.Preparing() },
        { TicketStatus.Preparing(), TicketStatus.Ready() }
    };

    // Required for EF
    private Ticket()
    {
    }
}