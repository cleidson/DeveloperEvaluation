using Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchsFeature.GetBranch;
using FluentValidation;
using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.GetBranch
{
    /// <summary>
    /// Validator for GetBranchRequest.
    /// </summary>
    public class GetBranchRequestValidator : AbstractValidator<GetBranchRequest>
    {
        public GetBranchRequestValidator()
        {
            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("O ID da filial é obrigatório.");
        }
    }
}
