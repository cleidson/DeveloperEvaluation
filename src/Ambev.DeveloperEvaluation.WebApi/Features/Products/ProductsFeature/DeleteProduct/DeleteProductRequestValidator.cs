using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.DeleteProduct;

/// <summary>
/// Validator for DeleteProductRequest.
/// </summary>
public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    public DeleteProductRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.")
            .NotEqual(Guid.Empty).WithMessage("O ID do produto não pode ser vazio.");
    }
}
