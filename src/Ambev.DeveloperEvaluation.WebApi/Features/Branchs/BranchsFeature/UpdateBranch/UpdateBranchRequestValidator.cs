using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.UpdateBranch
{
    /// <summary>
    /// Validator for UpdateBranchRequest.
    /// </summary>
    public class UpdateBranchRequestValidator : AbstractValidator<UpdateBranchRequest>
    {
        public UpdateBranchRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da filial é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da filial não pode ter mais de 100 caracteres.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("O endereço da filial é obrigatório.")
                .MaximumLength(200).WithMessage("O endereço da filial não pode ter mais de 200 caracteres.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("A localização da filial é obrigatória.")
                .MaximumLength(200).WithMessage("A localização da filial não pode ter mais de 200 caracteres.");
        }
    }
}
