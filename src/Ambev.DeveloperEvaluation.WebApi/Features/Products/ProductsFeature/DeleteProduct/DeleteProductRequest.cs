namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.DeleteProduct;

/// <summary>
/// Request model for deleting a product.
/// </summary>
public class DeleteProductRequest
{
    public Guid ProductId { get; set; }
}
