﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature;

/// <summary>
/// Validator for ProductRequest.
/// </summary>
public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(100)
            .WithMessage("Product name must be at most 100 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");
    }
}