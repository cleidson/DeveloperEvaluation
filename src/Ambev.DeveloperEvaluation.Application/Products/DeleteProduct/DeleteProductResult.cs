namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Result of a product deletion.
/// </summary>
public class DeleteProductResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid? ProductId { get; set; } // Pode ser null se o produto não for encontrado
}
