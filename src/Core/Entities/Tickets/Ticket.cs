using Amazon.DynamoDBv2.DataModel;
using Entities.SeedWork;
using Entities.Tickets.Validators;

namespace Entities.Tickets;

[DynamoDBTable("tickes_table")]
public  class Ticket : Entity, IAggregatedRoot
{

    [DynamoDBRangeKey("sk")]
    public Guid OrderId { get; private set; }

    [DynamoDBIgnore] 
    public TicketStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public List<TicketItem> TicketItems { get; private set; }
    public string TicketStatusString { get; private set; }

    public Ticket() { }
    public Ticket(Guid orderId, IEnumerable<TicketItem> ticketItems)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Status = TicketStatus.Received();
        TicketItems = ticketItems.ToList();
        CreatedAt = UpdatedAt = DateTime.UtcNow;
        TicketStatusString = Status.ToString();
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

    public bool IsItemsNotEmpty() => TicketItems.Any();

    private static readonly IValidator<Ticket> Validator = new TicketValidator();
}