using FluentValidation;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    /// <summary>
    /// Valida os parâmetros da query de listagem de vendas.
    /// </summary>
    public class GetSalesValidator : AbstractValidator<GetSalesQuery>
    {
        public GetSalesValidator()
        {
            RuleFor(q => q.StartDate)
                .LessThanOrEqualTo(q => q.EndDate)
                .When(q => q.StartDate.HasValue && q.EndDate.HasValue)
                .WithMessage("A data de início deve ser anterior ou igual à data de término.");
        }
    }
}
