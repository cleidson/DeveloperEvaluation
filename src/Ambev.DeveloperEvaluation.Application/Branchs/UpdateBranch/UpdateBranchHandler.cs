using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch
{
    /// <summary>
    /// Handles the UpdateBranchCommand.
    /// </summary>
    public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, UpdateBranchResult>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IValidator<UpdateBranchCommand> _validator;

        public UpdateBranchHandler(IBranchRepository branchRepository, IValidator<UpdateBranchCommand> validator)
        {
            _branchRepository = branchRepository;
            _validator = validator;
        }

        public async Task<UpdateBranchResult> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new UpdateBranchResult
                {
                    Success = false,
                    Message = "Requisição inválida: " + string.Join("; ", validationResult.Errors)
                };
            }

            var branch = await _branchRepository.GetByIdAsync(request.BranchId);
            if (branch == null)
            {
                return new UpdateBranchResult
                {
                    Success = false,
                    Message = "Filial não encontrada."
                };
            }

            branch.Name = request.Name;
            branch.Address = request.Address;
            branch.Location = request.Location;

            await _branchRepository.UpdateAsync(branch);

            return new UpdateBranchResult
            {
                Success = true,
                Message = "Filial atualizada com sucesso.",
                BranchId = request.BranchId
            };
        }
    }
}
