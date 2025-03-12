using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.GetProducts
{
    /// <summary>
    /// Validator for GetProductsRequest.
    /// </summary>
    public class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
    {
        public GetProductsRequestValidator()
        {
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).WithMessage("O preço mínimo deve ser maior ou igual a zero.");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(x => x.MinPrice)
                .WithMessage("O preço máximo deve ser maior ou igual ao preço mínimo.");
        }
    }
}
