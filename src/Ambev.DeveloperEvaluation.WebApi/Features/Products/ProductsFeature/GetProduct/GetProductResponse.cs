namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.GetProduct;

/// <summary>
/// Response model for retrieving a single product.
/// </summary>
public class GetProductResponse
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
