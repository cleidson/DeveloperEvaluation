using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature.GetSale
{
    /// <summary>
    /// Validator for GetSaleRequest.
    /// </summary>
    public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
    {
        public GetSaleRequestValidator()
        {
            RuleFor(x => x.SaleId)
                .NotEmpty().WithMessage("O ID da venda é obrigatório.");
        }
    }
}
