namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature;

/// <summary>
/// Represents the request model for creating or updating a branch.
/// </summary>
public class BranchRequest
{
    /// <summary>
    /// Gets or sets the branch name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch location.
    /// </summary>
    public string Location { get; set; } = string.Empty;
}