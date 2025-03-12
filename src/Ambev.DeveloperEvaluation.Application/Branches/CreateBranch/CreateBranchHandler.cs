using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Handles the CreateBranchCommand.
/// </summary>
public class CreateBranchHandler : IRequestHandler<CreateBranchCommand, Guid>
{
    private readonly IBranchRepository _branchRepository;

    public CreateBranchHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<Guid> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Address = request.Address
        };

        await _branchRepository.AddAsync(branch);
        return branch.Id;
    }
}
