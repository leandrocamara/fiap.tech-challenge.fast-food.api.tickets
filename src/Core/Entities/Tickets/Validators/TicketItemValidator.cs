using Entities.SeedWork;

namespace Entities.Tickets.Validators;

public class TicketItemValidator : IValidator<TicketItem>
{
    public bool IsValid(TicketItem instance, out string error)
    {
        error = "Quantity must be greater than zero";
        return instance.IsNotNegative();
    }
}