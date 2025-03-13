using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Command to update a product.
/// </summary>
public class UpdateProductCommand : IRequest<UpdateProductResult>
{
    public Guid ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}
