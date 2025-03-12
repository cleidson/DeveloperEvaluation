namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature;

/// <summary>
/// Represents the response returned after a branch is created or retrieved.
/// </summary>
public sealed class BranchResponse
{
    public Guid BranchId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}