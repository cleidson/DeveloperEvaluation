using Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.GetBranchs;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.GetBranchs;
using FluentValidation;
using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.GetBranchs
{
    /// <summary>
    /// Validator for GetBranchsRequest.
    /// Ensures that filters are correctly provided.
    /// </summary>
    public class GetBranchsRequestValidator : AbstractValidator<GetBranchsRequest>
    {
        public GetBranchsRequestValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("O nome da filial não pode ter mais de 100 caracteres.");

            RuleFor(x => x.Location)
                .MaximumLength(200).WithMessage("A localização da filial não pode ter mais de 200 caracteres.");
        }
    }
}
