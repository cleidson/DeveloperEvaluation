namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.CreateBranch;

/// <summary>
/// Represents the request model for creating or updating a branch.
/// </summary>
public class CreateBranchRequest
{
    /// <summary>
    /// Gets or sets the branch name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch location.
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch address.
    /// </summary>
    public string Address { get; set; } = string.Empty;
}
