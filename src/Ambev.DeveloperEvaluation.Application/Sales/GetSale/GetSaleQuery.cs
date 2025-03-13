using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Query to retrieve a sale by ID.
/// </summary>
public class GetSaleQuery : IRequest<GetSaleResult>
{
    public Guid SaleId { get; set; }
}
