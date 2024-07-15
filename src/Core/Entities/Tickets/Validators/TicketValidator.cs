using Entities.SeedWork;

namespace Entities.Tickets.Validators;

public class TicketValidator : IValidator<Ticket>
{
    public bool IsValid(Ticket instance, out string error)
    {
        error = "List of itens is empty";
        return instance.IsItemsNotEmpty();
    }
}