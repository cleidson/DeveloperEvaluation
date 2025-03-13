using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature;

/// <summary>
/// Response model for updating a sale.
/// </summary>
public class UpdateSaleResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid SaleId { get; set; }
}
