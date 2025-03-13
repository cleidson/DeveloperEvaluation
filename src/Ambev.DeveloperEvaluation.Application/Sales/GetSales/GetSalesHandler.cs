using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    /// <summary>
    /// Handler para recuperar todas as vendas com filtros opcionais.
    /// </summary>
    public class GetSalesHandler : IRequestHandler<GetSalesQuery, List<GetSalesResult>>
    {
        private readonly ISaleRepository _saleRepository;

        public GetSalesHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<List<GetSalesResult>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllAsync();

            if (request.SaleNumber is not null)
                sales = sales.Where(s => s.SaleNumber == request.SaleNumber).ToList();

            if (request.CustomerId.HasValue)
                sales = sales.Where(s => s.CustomerId == request.CustomerId.Value).ToList();

            if (request.BranchId.HasValue)
                sales = sales.Where(s => s.BranchId == request.BranchId.Value).ToList();

            if (request.StartDate.HasValue)
                sales = sales.Where(s => s.SaleDate >= request.StartDate.Value).ToList();

            if (request.EndDate.HasValue)
                sales = sales.Where(s => s.SaleDate <= request.EndDate.Value).ToList();

            if (request.IsCancelled.HasValue)
                sales = sales.Where(s => s.IsCancelled == request.IsCancelled.Value).ToList();

            return sales.Select(sale => new GetSalesResult
            {
                SaleId = sale.Id,
                SaleDate = sale.SaleDate,
                SaleNumber = sale.SaleNumber?? "",
                CustomerId = sale.CustomerId,
                BranchId = sale.BranchId,
                TotalAmount = sale.TotalAmount,
                IsCancelled = sale.IsCancelled,
                SaleItems = sale.SaleItems?.Select(item => new GetSalesItemResult // Evita NullReferenceException
                {
                    ProductId = item?.ProductId ?? Guid.Empty,
                    ProductName = item?.Product?.Name ?? "Desconhecido",
                    Quantity = item?.Quantity ?? 0,
                    UnitPrice = item?.UnitPrice ?? 0,
                    Discount = item?.Discount ?? 0,
                    TotalPrice = item?.TotalPrice ?? 0
                }).ToList() ?? new List<GetSalesItemResult>() // Se `SaleItems` for null, retorna lista vazia
            }).ToList();
        }
    }
}
