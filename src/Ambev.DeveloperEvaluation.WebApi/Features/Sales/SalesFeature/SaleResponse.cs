namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature;

/// <summary>
/// Represents the response returned after a sale is created or retrieved.
/// </summary>
public sealed class SaleResponse
{
    public Guid SaleId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public List<SaleItemResponse> SaleItems { get; set; } = new();
}

/// <summary>
/// Represents a sale item in the response.
/// </summary>
public class SaleItemResponse
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
}
