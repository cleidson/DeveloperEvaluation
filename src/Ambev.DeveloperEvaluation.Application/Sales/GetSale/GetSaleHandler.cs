using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Handles the GetSaleQuery.
/// </summary>
public class GetSaleHandler : IRequestHandler<GetSaleQuery, Sale>
{
    private readonly ISaleRepository _saleRepository;

    public GetSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Sale> Handle(GetSaleQuery request, CancellationToken cancellationToken)
    {
        return await _saleRepository.GetByIdAsync(request.SaleId);
    }
}
