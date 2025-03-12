using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
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
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Sale>
{
    private readonly ISaleRepository _saleRepository;

    public CreateSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Sale> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Sale
        {
            CustomerId = request.CustomerId,
            BranchId = request.BranchId,
            SaleItems = request.SaleItems,
            SaleDate = DateTime.UtcNow,
            Status = SaleStatus.Pending
        };

        ApplyDiscounts(sale);

        await _saleRepository.AddAsync(sale);
        return sale;
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
}
