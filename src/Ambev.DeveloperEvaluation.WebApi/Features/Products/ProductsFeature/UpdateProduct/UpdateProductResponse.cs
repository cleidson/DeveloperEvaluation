using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.UpdateProduct;

/// <summary>
/// Response for updating a product.
/// </summary>
public class UpdateProductResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
}
