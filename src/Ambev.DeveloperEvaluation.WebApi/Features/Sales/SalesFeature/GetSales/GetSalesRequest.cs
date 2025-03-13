namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature.GetSales;

public class GetSalesRequest
{
    public Guid? CustomerId { get; set; }
    public Guid? BranchId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsCancelled { get; set; }
}