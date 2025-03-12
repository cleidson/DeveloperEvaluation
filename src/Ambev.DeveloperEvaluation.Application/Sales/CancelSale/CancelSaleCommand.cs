using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command to cancel a sale.
/// </summary>
public class CancelSaleCommand : IRequest<bool>
{
    public Guid SaleId { get; set; }
}
