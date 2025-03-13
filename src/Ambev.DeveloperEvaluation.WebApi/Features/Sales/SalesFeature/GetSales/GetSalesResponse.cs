namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature.GetSales;

/// <summary>
/// Response para a listagem de vendas.
/// </summary>
public class GetSalesResponse
{
    public List<GetSalesItemResponse> Sales { get; set; } = new();
}

public class GetSalesItemResponse
{
    public Guid SaleId { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
}