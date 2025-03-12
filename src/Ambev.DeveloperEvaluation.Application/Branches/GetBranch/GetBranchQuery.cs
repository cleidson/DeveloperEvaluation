using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

/// <summary>
/// Query to retrieve a branch by ID.
/// </summary>
public class GetBranchQuery : IRequest<Branch>
{
    public Guid BranchId { get; set; }
}
