using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Command to delete a product.
/// </summary>
public class DeleteProductCommand : IRequest<DeleteProductResult>
{
    public Guid ProductId { get; set; }
}
