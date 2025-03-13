using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature.GetSale;

/// <summary>
/// Response model for retrieving a sale.
/// </summary>
public class GetSaleResponse
{
    public Guid SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public string SaleNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public List<GetSaleItemResponse> SaleItems { get; set; } = new();
}

/// <summary>
/// Represents an individual sale item in the sale response.
/// </summary>
public class GetSaleItemResponse
{
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
}
