using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.GetProduct;

/// <summary>
/// Validator for GetProductRequest.
/// Ensures that the request contains a valid ProductId.
/// </summary>
public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    public GetProductRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.")
            .NotEqual(Guid.Empty).WithMessage("O ID do produto não pode ser um GUID vazio.");
    }
}
