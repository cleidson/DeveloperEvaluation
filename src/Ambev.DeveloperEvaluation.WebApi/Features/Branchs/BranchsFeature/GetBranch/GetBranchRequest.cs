using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchsFeature.GetBranch
{
    /// <summary>
    /// Represents the request parameters for getting a branch by ID.
    /// </summary>
    public class GetBranchRequest
    {
        public Guid BranchId { get; set; }
    }
}
