using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.DeleteBranch
{
    /// <summary>
    /// Request model for deleting a branch.
    /// </summary>
    public class DeleteBranchRequest
    {
        public Guid BranchId { get; set; }
    }
}
