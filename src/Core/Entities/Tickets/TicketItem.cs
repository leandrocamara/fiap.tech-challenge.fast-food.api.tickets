using Entities.SeedWork;
using Entities.Tickets.Validators;

namespace Entities.Tickets;

public class TicketItem : Entity
{
    public Product Product { get; private set; }
    public short Quantity { get; private set; }

    public TicketItem(Product product, short quantity)
    {
        Id = Guid.NewGuid();
        Product = product;
        Quantity = quantity;

        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    private static readonly IValidator<TicketItem> Validator = new TicketItemValidator();
}