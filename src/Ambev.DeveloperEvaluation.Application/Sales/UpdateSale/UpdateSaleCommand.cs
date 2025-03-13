using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command to update a sale.
/// </summary>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid SaleId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public List<UpdateSaleItemCommand> SaleItems { get; set; } = new();
}

/// <summary>
/// Represents an item in the sale update request.
/// </summary>
public class UpdateSaleItemCommand
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
