using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command to create a sale.
    /// </summary>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public List<SaleItem> SaleItems { get; set; } = new();
    }
}
