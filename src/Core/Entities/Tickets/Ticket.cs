using Amazon.DynamoDBv2.DataModel;
using Entities.SeedWork;
using Entities.Tickets.Validators;

namespace Entities.Tickets;

[DynamoDBTable("tickes_table")]
public sealed class Ticket : Entity, IAggregatedRoot
{
    [DynamoDBRangeKey("sk")]
    public Guid OrderId { get; private set; }
    public TicketStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IEnumerable<TicketItem> TicketItems { get; private set; }

    public Ticket(Guid orderId, IEnumerable<TicketItem> ticketItems)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Status = TicketStatus.Received();
        TicketItems = ticketItems;
        CreatedAt = UpdatedAt = DateTime.UtcNow;

        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    public void UpdateStatus()
    {
        if (StatusSequence.TryGetValue(Status, out var nextStatus))
        {
            Status = nextStatus;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    private static readonly Dictionary<TicketStatus, TicketStatus> StatusSequence = new()
    {
        { TicketStatus.Received(), TicketStatus.Preparing() },
        { TicketStatus.Preparing(), TicketStatus.Ready() }
    };

    public bool IsItemsEmpty() => TicketItems.Any();

    private static readonly IValidator<Ticket> Validator = new TicketValidator();
}