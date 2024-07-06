using Entities.SeedWork;
using Entities.Tickets.Validators;

namespace Entities.Tickets;

public readonly struct Product
{
    public Guid Id { get; }
    public string Name { get; }
    public string Category { get; }
    public string Description { get; }

    public Product(Guid id, string name, string category, string description)
    {
        Id = id;
        Name = name;
        Category = category;
        Description = description;

        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    private static readonly IValidator<Product> Validator = new ProductValidator();
}