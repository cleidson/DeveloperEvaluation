using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Branchs.GetBranch;

/// <summary>
/// Handles the GetBranchQuery.
/// </summary>
public class GetBranchHandler : IRequestHandler<GetBranchQuery, Branch>
{
    private readonly IBranchRepository _branchRepository;

    public GetBranchHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<Branch> Handle(GetBranchQuery request, CancellationToken cancellationToken)
    {
        return await _branchRepository.GetByIdAsync(request.BranchId);
    }
}
