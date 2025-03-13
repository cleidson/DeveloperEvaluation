using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature.GetSales;

/// <summary>
/// Validator for GetSalesRequest.
/// </summary>
public class GetSalesRequestValidator : AbstractValidator<GetSalesRequest>
{
    public GetSalesRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("O ID do cliente é obrigatório.");

    }
}
