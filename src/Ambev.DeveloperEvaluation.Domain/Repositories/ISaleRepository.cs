using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Defines the contract for sale repository.
/// </summary>
public interface ISaleRepository
{
    Task<Sale> GetByIdAsync(Guid id);
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<List<Sale>> GetSalesByProductIdAsync(Guid productId);
    Task AddAsync(Sale sale);
    Task UpdateAsync(Sale sale);
}
