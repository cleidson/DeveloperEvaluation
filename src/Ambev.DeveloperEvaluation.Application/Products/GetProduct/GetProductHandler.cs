using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Handles the GetProductQuery.
/// </summary>
public class GetProductHandler : IRequestHandler<GetProductQuery, GetProductResult?>
{
    private readonly IProductRepository _productRepository;

    public GetProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductResult?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product == null)
        {
            // Retorna null ao invés de lançar uma exceção
            return null;
        }

        return new GetProductResult
        {
            ProductId = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
}
