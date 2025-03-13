using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature
{
    /// <summary>
    /// Request model for creating a sale.
    /// </summary>
    public class CreateSaleRequest
    {
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public List<CreateSaleItemRequest> SaleItems { get; set; } = new(); 
    }

    /// <summary>
    /// Represents an item in a sale request.
    /// </summary>
    public class CreateSaleItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
