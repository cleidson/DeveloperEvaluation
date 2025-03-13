using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handles the CancelSaleCommand.
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, bool>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;

    public CancelSaleHandler(ISaleRepository saleRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
        _mediator = mediator;
    }

    public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId);
        if (sale == null)
            throw new InvalidOperationException("Venda não encontrada.");

        sale.Status = SaleStatus.Cancelled;
        sale.IsCancelled = true;
        await _saleRepository.UpdateAsync(sale);
        await _mediator.Publish(new SaleCancelledEvent(sale.Id), cancellationToken);

        return true;
    }
}
