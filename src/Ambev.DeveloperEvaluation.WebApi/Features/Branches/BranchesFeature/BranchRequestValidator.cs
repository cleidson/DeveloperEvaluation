using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature;

/// <summary>
/// Validator for BranchRequest.
/// </summary>
public class BranchRequestValidator : AbstractValidator<BranchRequest>
{
    public BranchRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Branch name is required.")
            .MaximumLength(100)
            .WithMessage("Branch name must be at most 100 characters.");

        RuleFor(x => x.Location)
            .NotEmpty()
            .WithMessage("Branch location is required.")
            .MaximumLength(200)
            .WithMessage("Branch location must be at most 200 characters.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Branch address is required.")
            .MaximumLength(200)
            .WithMessage("Branch address must be at most 200 characters.");
    }
}

