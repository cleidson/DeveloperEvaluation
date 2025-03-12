using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Command to create a branch.
/// </summary>
public class CreateBranchCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public string? Address { get; set; }
}
