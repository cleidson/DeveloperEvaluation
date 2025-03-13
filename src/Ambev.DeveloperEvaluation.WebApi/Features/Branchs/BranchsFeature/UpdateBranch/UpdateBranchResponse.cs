using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.UpdateBranch
{
    /// <summary>
    /// Response model for updating a branch.
    /// </summary>
    public class UpdateBranchResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
    }
}
