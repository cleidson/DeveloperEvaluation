using Ambev.DeveloperEvaluation.Application.Branchs.GetBranch;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Branchs.GetBranchs
{
  
    /// <summary>
    /// Handles the GetBranchesQuery to retrieve a list of branches.
    /// </summary>
    public class GetBranchsHandler : IRequestHandler<GetBranchsQuery, List<GetBranchsResult>>
    {
        private readonly IBranchRepository _branchRepository;

        public GetBranchsHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<List<GetBranchsResult>> Handle(GetBranchsQuery request, CancellationToken cancellationToken)
        {
            var branches = await _branchRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(request.Name))
                branches = branches.Where(b => b.Name.Contains(request.Name));

            if (!string.IsNullOrEmpty(request.Location))
                branches = branches.Where(b => b.Location.Contains(request.Location));

            return branches.Select(b => new GetBranchsResult
            {
                BranchId = b.Id,
                Name = b.Name,
                Address = b.Address,
                Location = b.Location
            }).ToList();
        }
    }
}
