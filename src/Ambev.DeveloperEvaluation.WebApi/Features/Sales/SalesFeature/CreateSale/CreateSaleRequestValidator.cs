using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature
{
    /// <summary>
    /// Validator for CreateSaleRequest.
    /// </summary>
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("O ID do cliente é obrigatório.");

            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("O ID da filial é obrigatório.");

            RuleFor(x => x.SaleItems)
                .NotEmpty().WithMessage("A venda deve conter pelo menos um item.");

            RuleForEach(x => x.SaleItems).SetValidator(new CreateSaleItemRequestValidator());
        }
    }

    /// <summary>
    /// Validator for sale items.
    /// </summary>
    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("O ID do produto é obrigatório.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.")
                .LessThanOrEqualTo(20).WithMessage("Não é possível vender mais de 20 unidades do mesmo produto.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");
        }
    }
}
