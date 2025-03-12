namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.CreateProduct;

/// <summary>
/// Represents the request model for creating or updating a product.
/// </summary>
public class CreateProductRequest
{
    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product price.
    /// </summary>
    public decimal Price { get; set; }
}