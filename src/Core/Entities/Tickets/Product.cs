using Amazon.DynamoDBv2.DataModel;
using Entities.SeedWork;
using Entities.Tickets.Validators;

namespace Entities.Tickets;
[DynamoDBTable("tickets_table")]
public  class Product
{
    public Guid Id { get; protected set; }
  
    public string Name { get; set; }
    
    public string Category { get; set; }
    
    public string Description { get; set; }

    public Product() { }

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