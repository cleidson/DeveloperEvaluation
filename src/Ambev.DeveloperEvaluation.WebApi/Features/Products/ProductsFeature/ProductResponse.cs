namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature;


/// <summary>
/// Represents the response returned after a product is created or retrieved.
/// </summary>
public sealed class ProductResponse
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}