using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand.
/// </summary>
public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.ProductId)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do produto não pode ter mais de 100 caracteres.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");
    }
}
