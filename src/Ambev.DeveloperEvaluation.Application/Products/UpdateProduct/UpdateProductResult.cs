namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Represents the result of updating a product.
/// </summary>
public class UpdateProductResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
}
