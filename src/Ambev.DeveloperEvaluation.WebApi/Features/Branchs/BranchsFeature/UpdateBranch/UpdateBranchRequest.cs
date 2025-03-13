using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.UpdateBranch
{
    /// <summary>
    /// Request model for updating a branch.
    /// </summary>
    public class UpdateBranchRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
