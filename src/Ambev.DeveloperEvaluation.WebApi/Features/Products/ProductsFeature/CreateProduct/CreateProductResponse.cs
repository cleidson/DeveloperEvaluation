namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.CreateProduct;


/// <summary>
/// Represents the response returned after a product is created or retrieved.
/// </summary>
public sealed class CreateProductResponse
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}