using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleCommand.
/// Ensures that the sale ID and total amount are valid.
/// </summary>
public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleValidator()
    {
        RuleFor(s => s.SaleId)
            .NotEmpty().WithMessage("O ID da venda é obrigatório.");

        RuleFor(s => s.TotalAmount)
            .GreaterThan(0).WithMessage("O valor total da venda deve ser maior que zero.");
    }
}
