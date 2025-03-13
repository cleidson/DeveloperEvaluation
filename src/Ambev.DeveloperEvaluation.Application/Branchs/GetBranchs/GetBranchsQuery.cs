using Ambev.DeveloperEvaluation.Application.Branchs.GetBranch;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Branchs.GetBranchs
{
    /// <summary>
    /// Query to retrieve a list of branches with optional filters.
    /// </summary>
    public class GetBranchsQuery : IRequest<List<GetBranchsResult>>
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
    }
}
