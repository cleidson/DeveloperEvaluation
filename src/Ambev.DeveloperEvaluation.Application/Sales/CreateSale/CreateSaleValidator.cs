using FluentValidation;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Validator for the CreateSaleCommand.
    /// </summary>
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(s => s.CustomerId)
                .NotEmpty().WithMessage("O ID do cliente é obrigatório.");

            RuleFor(s => s.BranchId)
                .NotEmpty().WithMessage("O ID da filial é obrigatório.");

            RuleFor(s => s.SaleItems)
                .NotEmpty().WithMessage("A venda deve conter pelo menos um item.")
                .Must(items => items.All(i => i.Quantity > 0))
                .WithMessage("Cada item deve ter uma quantidade maior que zero.")
                .Must(items => items.All(i => i.Quantity <= 20))
                .WithMessage("Nenhum item pode ter mais de 20 unidades.")
                .Must(items => items.All(i => i.UnitPrice > 0))
                .WithMessage("O preço unitário deve ser maior que zero.")
                .Must(items => items.All(i => i.ProductId != Guid.Empty))
                .WithMessage("Cada item deve ter um ID de produto válido.");
        }
    }
}
