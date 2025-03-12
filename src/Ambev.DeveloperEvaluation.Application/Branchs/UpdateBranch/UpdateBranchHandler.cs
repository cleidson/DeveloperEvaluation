using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch;

/// <summary>
/// Handles the UpdateBranchCommand.
/// </summary>
public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, bool>
{
    private readonly IBranchRepository _branchRepository;

    public UpdateBranchHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<bool> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(request.BranchId);
        if (branch == null)
            throw new InvalidOperationException("Filial não encontrada.");

        branch.Name = request.Name;
        branch.Address = request.Address;

        await _branchRepository.UpdateAsync(branch);
        return true;
    }
}
