﻿using Entities.SeedWork;

namespace Entities.Tickets.Validators;

public class ProductValidator  : IValidator<Product>
{
    public bool IsValid(Product instance, out string error)
    {
        error = string.Empty;
        return true;
    }
}