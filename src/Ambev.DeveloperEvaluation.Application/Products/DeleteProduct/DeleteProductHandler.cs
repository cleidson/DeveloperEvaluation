using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Handles the DeleteProductCommand.
/// </summary>
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly DeleteProductValidator _validator;

    public DeleteProductHandler(IProductRepository productRepository, ISaleRepository saleRepository)
    {
        _productRepository = productRepository;
        _validator = new DeleteProductValidator(saleRepository);
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new InvalidOperationException(string.Join("; ", validationResult.Errors));

        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
            throw new InvalidOperationException("Produto não encontrado.");

        await _productRepository.DeleteAsync(product);
        return true;
    }
}
