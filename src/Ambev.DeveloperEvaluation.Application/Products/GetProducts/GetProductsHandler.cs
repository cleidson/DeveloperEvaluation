using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    /// <summary>
    /// Handles the GetProductsQuery to retrieve a list of products.
    /// </summary>
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<GetProductsResult>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<GetProductsResult>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            // Aplica os filtros opcionais
            if (!string.IsNullOrEmpty(request.Name))
                products = products.Where(p => p.Name.Contains(request.Name, System.StringComparison.OrdinalIgnoreCase));

            if (request.MinPrice.HasValue)
                products = products.Where(p => p.Price >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                products = products.Where(p => p.Price <= request.MaxPrice.Value);

            // Mapeia os produtos para GetProductsResult
            return products
                .Select(p => new GetProductsResult
                {
                    ProductId = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();
        }
    }
}
