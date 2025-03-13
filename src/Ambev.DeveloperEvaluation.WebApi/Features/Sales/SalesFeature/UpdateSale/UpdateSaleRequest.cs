using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature;

/// <summary>
/// Request model for updating a sale.
/// </summary>
public class UpdateSaleRequest
{
    public Guid SaleId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public List<UpdateSaleItemRequest> Items { get; set; } = new();
}

/// <summary>
/// Represents an item in a sale update request.
/// </summary>
public class UpdateSaleItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
