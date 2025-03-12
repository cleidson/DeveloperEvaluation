using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command to create a product.
/// </summary>
public class CreateProductCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
}
