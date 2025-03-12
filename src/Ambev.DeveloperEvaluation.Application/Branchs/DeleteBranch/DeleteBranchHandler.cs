using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Branchs.DeleteBranch;

/// <summary>
/// Handles the DeleteBranchCommand.
/// </summary>
public class DeleteBranchHandler : IRequestHandler<DeleteBranchCommand, bool>
{
    private readonly IBranchRepository _branchRepository;

    public DeleteBranchHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<bool> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(request.BranchId);
        if (branch == null)
            throw new InvalidOperationException("Filial não encontrada.");

        await _branchRepository.DeleteAsync(branch);
        return true;
    }
}
