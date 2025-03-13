using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Branchs.DeleteBranch
{
    /// <summary>
    /// Validator for DeleteBranchCommand.
    /// </summary>
    public class DeleteBranchValidator : AbstractValidator<DeleteBranchCommand>
    {
        private readonly IBranchRepository _branchRepository;

        public DeleteBranchValidator(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;

            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("O ID da filial é obrigatório.")
                .MustAsync(BranchExists).WithMessage("A filial informada não existe.");
        }

        private async Task<bool> BranchExists(Guid branchId, CancellationToken cancellationToken)
        {
            return await _branchRepository.ExistsAsync(branchId);
        }
    }
}
