using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;


/// <summary>
/// Represents the result of retrieving a product.
/// </summary>
public class GetProductResult
{
    public Guid ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}