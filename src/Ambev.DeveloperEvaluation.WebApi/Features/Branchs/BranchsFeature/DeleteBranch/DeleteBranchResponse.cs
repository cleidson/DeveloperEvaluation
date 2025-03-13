namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.DeleteBranch
{
    /// <summary>
    /// Response model for branch deletion.
    /// </summary>
    public class DeleteBranchResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
