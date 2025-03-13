using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Represents the result of updating a sale.
    /// </summary>
    public class UpdateSaleResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
