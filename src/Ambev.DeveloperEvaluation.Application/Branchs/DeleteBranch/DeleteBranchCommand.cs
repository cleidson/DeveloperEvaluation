using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Branchs.DeleteBranch;

/// <summary>
/// Command to delete a branch.
/// </summary>
public class DeleteBranchCommand : IRequest<bool>
{
    public Guid BranchId { get; set; }
}
