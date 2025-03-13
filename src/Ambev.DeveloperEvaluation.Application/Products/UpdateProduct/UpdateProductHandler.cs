using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handles the UpdateProductCommand.
/// </summary>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            return new UpdateProductResult
            {
                Success = false,
                Message = "Produto não encontrado."
            };
        }

        product.Name = request.Name;
        product.Price = request.Price;

        await _productRepository.UpdateAsync(product);

        return new UpdateProductResult
        {
            Success = true,
            Message = "Produto atualizado com sucesso.",
            ProductId = product.Id
        };
    }
}
