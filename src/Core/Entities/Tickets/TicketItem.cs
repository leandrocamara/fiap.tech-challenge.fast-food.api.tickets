using Amazon.DynamoDBv2.DataModel;
using Entities.SeedWork;
using Entities.Tickets.Validators;

namespace Entities.Tickets;
[DynamoDBTable("tickes_table")]
public class TicketItem
{
    public Guid Id { get; protected set; }
    public Product Product { get; private set; }
    public short Quantity { get; private set; }
    public TicketItem() { }
    public TicketItem(Product product, short quantity)
    {
        Id = Guid.NewGuid();
        Product = product;
        Quantity = quantity;

        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    public bool IsNotNegative() => Quantity > 0;

    private static readonly IValidator<TicketItem> Validator = new TicketItemValidator();
}