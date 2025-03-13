using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.UpdateProduct;

/// <summary>
/// Request for updating a product.
/// </summary>
public class UpdateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
