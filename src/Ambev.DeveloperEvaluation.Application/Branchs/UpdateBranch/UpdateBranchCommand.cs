using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch;

/// <summary>
/// Command to update a branch.
/// </summary>
public class UpdateBranchCommand : IRequest<bool>
{
    public Guid BranchId { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
}
