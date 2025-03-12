using System;

namespace Ambev.DeveloperEvaluation.Application.Branchs.GetBranch;

/// <summary>
/// Represents the result of retrieving a branch.
/// </summary>
public class GetBranchResult
{
    public Guid BranchId { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
}
