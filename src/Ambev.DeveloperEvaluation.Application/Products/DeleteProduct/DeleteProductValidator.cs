using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Validator for DeleteProductCommand.
/// Ensures that the product is not used in any sales before deletion.
/// </summary>
public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    private readonly ISaleRepository _saleRepository;

    public DeleteProductValidator(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;

        RuleFor(p => p.ProductId)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.")
            .MustAsync(ProductNotUsedInSales).WithMessage("O produto não pode ser excluído pois está em uma venda ativa.");
    }

    private async Task<bool> ProductNotUsedInSales(Guid productId, CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.GetSalesByProductIdAsync(productId);
        return sales.Count == 0; // Só pode excluir se não houver vendas associadas.
    }
}
