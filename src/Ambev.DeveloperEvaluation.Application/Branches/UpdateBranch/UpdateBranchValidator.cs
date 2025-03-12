using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

/// <summary>
/// Validator for UpdateBranchCommand.
/// Ensures that all required fields are provided.
/// </summary>
public class UpdateBranchValidator : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchValidator()
    {
        RuleFor(b => b.BranchId)
            .NotEmpty().WithMessage("O ID da filial é obrigatório.");

        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("O nome da filial é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da filial não pode ter mais de 100 caracteres.");

        RuleFor(b => b.Address)
            .NotEmpty().WithMessage("O endereço da filial é obrigatório.")
            .MaximumLength(200).WithMessage("O endereço da filial não pode ter mais de 200 caracteres.");
    }
}
