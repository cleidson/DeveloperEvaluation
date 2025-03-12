namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.GetProduct;

/// <summary>
/// Request model for retrieving a product by ID.
/// </summary>
public class GetProductRequest
{
    public Guid ProductId { get; set; }
}
