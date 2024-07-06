namespace Entities.Tickets;

public readonly struct TicketStatus
{
    private ETicketStatus Value { get; }

    public static TicketStatus Received() => new(ETicketStatus.Received);
    public static TicketStatus Preparing() => new(ETicketStatus.Preparing);
    public static TicketStatus Ready() => new(ETicketStatus.Ready);

    public static implicit operator short(TicketStatus status) => (short)status.Value;
    public static implicit operator TicketStatus(short value) => new((ETicketStatus)value);
    public static implicit operator string(TicketStatus status) => status.ToString();

    public override string ToString() => Value.ToString();

    private TicketStatus(ETicketStatus status) => Value = status;

    private enum ETicketStatus : short
    {
        Received = 1,
        Preparing = 2,
        Ready = 3
    }
}