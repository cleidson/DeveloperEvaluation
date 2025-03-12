using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Validator for CreateBranchCommand.
/// Ensures that all required fields are provided.
/// </summary>
public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("O nome da filial é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da filial não pode ter mais de 100 caracteres.");

        RuleFor(b => b.Address)
            .NotEmpty().WithMessage("O endereço da filial é obrigatório.")
            .MaximumLength(200).WithMessage("O endereço da filial não pode ter mais de 200 caracteres.");
    }
}
