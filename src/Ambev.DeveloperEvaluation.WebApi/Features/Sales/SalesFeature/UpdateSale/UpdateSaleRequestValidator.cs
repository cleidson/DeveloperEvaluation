using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature;

/// <summary>
/// Validator for UpdateSaleRequest.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty().WithMessage("O ID da venda é obrigatório.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("O ID do cliente é obrigatório.");

        RuleFor(x => x.BranchId)
            .NotEmpty().WithMessage("O ID da filial é obrigatório.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("A venda deve conter pelo menos um item.");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId).NotEmpty().WithMessage("O ID do produto é obrigatório.");
            item.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
            item.RuleFor(i => i.UnitPrice).GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");
        });
    }
}
