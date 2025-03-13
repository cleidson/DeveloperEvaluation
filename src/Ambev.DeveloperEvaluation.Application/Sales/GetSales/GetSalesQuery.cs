using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    /// <summary>
    /// Query para listar todas as vendas com filtros opcionais.
    /// </summary>
    public class GetSalesQuery : IRequest<List<GetSalesResult>>
    {
        public Guid? CustomerId { get; set; } 
        public string? SaleNumber { get; set; }
        public Guid? BranchId { get; set; } 
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }   
        public bool? IsCancelled { get; set; }   
    }
}
