using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch
{
    /// <summary>
    /// Command to update a branch.
    /// </summary>
    public class UpdateBranchCommand : IRequest<UpdateBranchResult>
    {
        public Guid BranchId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
