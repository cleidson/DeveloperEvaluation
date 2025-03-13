namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature
{
    public class CreateSaleResponse
    {
        public Guid SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
