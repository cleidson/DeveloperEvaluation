using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.DeleteBranch
{
    /// <summary>
    /// Validator for DeleteBranchRequest.
    /// </summary>
    public class DeleteBranchRequestValidator : AbstractValidator<DeleteBranchRequest>
    {
        public DeleteBranchRequestValidator()
        {
            RuleFor(b => b.BranchId)
                .NotEmpty().WithMessage("O ID da filial é obrigatório.");
        }
    }
}
