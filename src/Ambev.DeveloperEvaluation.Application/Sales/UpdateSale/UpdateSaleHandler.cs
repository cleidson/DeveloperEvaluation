using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handles the UpdateSaleCommand.
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;

    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        IBranchRepository branchRepository,
        IProductRepository productRepository,
        IMediator mediator)
    {
        _saleRepository = saleRepository;
        _branchRepository = branchRepository;
        _productRepository = productRepository;
        _mediator = mediator;
    }
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId);
        if (sale == null)
        {
            return new UpdateSaleResult { Success = false, Message = $"Venda com ID {request.SaleId} não encontrada." };
        }

        var branchExists = await _branchRepository.ExistsAsync(request.BranchId);
        if (!branchExists)
        {
            return new UpdateSaleResult { Success = false, Message = $"Filial com ID {request.BranchId} não encontrada." };
        }

        var invalidProductIds = new List<Guid>();
        foreach (var productId in request.SaleItems.Select(i => i.ProductId))
        {
            var exists = await _productRepository.ExistsAsync(productId);
            if (!exists)
            {
                invalidProductIds.Add(productId);
            }
        }

        if (invalidProductIds.Any())
        {
            return new UpdateSaleResult
            {
                Success = false,
                Message = $"Erro: Os seguintes produtos não foram encontrados: {string.Join(", ", invalidProductIds)}"
            };
        }

        // Atualizar os dados da venda
        sale.CustomerId = request.CustomerId;
        sale.BranchId = request.BranchId;

        // Removendo os itens antigos e adicionando os novos corretamente
        sale.SaleItems.Clear();

        sale.SaleItems.AddRange(request.SaleItems.Select(i => new SaleItem
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice,
            TotalPrice = i.Quantity * i.UnitPrice
        }));

        sale.TotalAmount = sale.SaleItems.Sum(i => i.TotalPrice);

        // Atualizar no banco
        await _saleRepository.UpdateAsync(sale);

        // Publicar evento de modificação
        await _mediator.Publish(new SaleModifiedEvent(sale.Id, sale.TotalAmount), cancellationToken);

        return new UpdateSaleResult
        {
            Success = true,
            Message = "Venda atualizada com sucesso.",
            SaleId = sale.Id,
            BranchId = sale.BranchId,
            CustomerId = sale.CustomerId,
            SaleDate = sale.SaleDate,
            TotalAmount = sale.TotalAmount
        };
    }

}
