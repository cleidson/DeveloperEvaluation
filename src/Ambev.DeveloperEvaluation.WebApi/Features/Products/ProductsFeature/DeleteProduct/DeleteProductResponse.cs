namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.DeleteProduct;

/// <summary>
/// Response model for delete product operation.
/// </summary>
public class DeleteProductResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
