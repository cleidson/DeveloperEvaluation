using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Defines the contract for branch repository.
/// </summary>
public interface IBranchRepository
{
    Task<Branch> GetByIdAsync(Guid id);
    Task<IEnumerable<Branch>> GetAllAsync();
    Task AddAsync(Branch branch);
    Task UpdateAsync(Branch branch);
    Task DeleteAsync(Branch branch);
    Task<bool> ExistsAsync(Guid id);
}
