using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command to update a sale.
/// </summary>
public class UpdateSaleCommand : IRequest<bool>
{
    public Guid SaleId { get; set; }
    public decimal TotalAmount { get; set; }
}
