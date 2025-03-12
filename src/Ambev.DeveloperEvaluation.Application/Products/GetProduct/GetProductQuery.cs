using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Query to retrieve a product by its ID.
/// </summary>
public class GetProductQuery : IRequest<GetProductResult>
{
    public Guid ProductId { get; set; }
}