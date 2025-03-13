using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handles the CreateSaleCommand.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;

    public CreateSaleHandler(ISaleRepository saleRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
    }
    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Sale
        { 
            CustomerId = request.CustomerId,
            BranchId = request.BranchId,
            SaleItems = request.SaleItems,
            SaleDate = DateTime.UtcNow,
            Status = SaleStatus.Pending, 
            RegisteredByUserId =  request.CustomerId,
            SaleNumber = GenerateRandomSaleNumber()

        };

        ApplyDiscounts(sale);
        await _saleRepository.AddAsync(sale);

        await _mediator.Publish(new SaleCreatedEvent(sale.Id, sale.SaleDate, sale.CustomerId, sale.TotalAmount), cancellationToken);


        return new CreateSaleResult
        {
            SaleId = sale.Id,
            SaleDate = sale.SaleDate,
            BranchId = sale.BranchId,
            CustomerId = sale.CustomerId,
            TotalAmount = sale.TotalAmount 
        };
    }

    /// <summary>
    /// Aplica os descontos e calcula o total da venda.
    /// </summary>
    private void ApplyDiscounts(Sale sale)
    {
        foreach (var item in sale.SaleItems)
        {
            // Regra: Não pode vender mais de 20 unidades do mesmo produto
            if (item.Quantity > 20)
                throw new InvalidOperationException($"Não é possível vender mais de 20 unidades do produto {item.ProductId}.");

            // Regra: Aplicação de descontos conforme a quantidade de itens
            if (item.Quantity >= 10)
            {
                item.Discount = item.UnitPrice * item.Quantity * 0.20m; // 20% de desconto
            }
            else if (item.Quantity >= 4)
            {
                item.Discount = item.UnitPrice * item.Quantity * 0.10m; // 10% de desconto
            }
            else
            {
                item.Discount = 0m; // Sem desconto
            }

            // Calcula o valor total do item já com desconto aplicado
            item.TotalPrice = (item.UnitPrice * item.Quantity) - item.Discount;
        }

        // Calcula o total da venda somando todos os itens
        sale.TotalAmount = sale.SaleItems.Sum(i => i.TotalPrice);
    }

    private string GenerateRandomSaleNumber()
    {
        return $"SALE-{Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper()}";
    }

}
