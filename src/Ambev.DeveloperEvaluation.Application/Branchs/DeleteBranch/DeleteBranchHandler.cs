using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Branchs.DeleteBranch
{
    /// <summary>
    /// Handles the DeleteBranchCommand.
    /// </summary>
    public class DeleteBranchHandler : IRequestHandler<DeleteBranchCommand, DeleteBranchResult>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IValidator<DeleteBranchCommand> _validator;

        public DeleteBranchHandler(IBranchRepository branchRepository, IValidator<DeleteBranchCommand> validator)
        {
            _branchRepository = branchRepository;
            _validator = validator;
        }

        public async Task<DeleteBranchResult> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var branch = await _branchRepository.GetByIdAsync(request.BranchId);
            if (branch == null)
            {
                throw new KeyNotFoundException("Filial não encontrada.");
            }

            await _branchRepository.DeleteAsync(branch);

            return new DeleteBranchResult { BranchId = request.BranchId };
        }
    }
}
