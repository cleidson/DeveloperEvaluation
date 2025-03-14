﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch
{
    /// <summary>
    /// Validator for UpdateBranchCommand.
    /// </summary>
    public class UpdateBranchValidator : AbstractValidator<UpdateBranchCommand>
    {
        public UpdateBranchValidator()
        {
            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("O ID da filial é obrigatório.");

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
