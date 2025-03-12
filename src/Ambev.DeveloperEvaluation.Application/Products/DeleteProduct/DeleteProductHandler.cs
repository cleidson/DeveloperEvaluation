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
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly DeleteProductValidator _validator;

    public DeleteProductHandler(IProductRepository productRepository, ISaleRepository saleRepository)
    {
        _productRepository = productRepository;
        _validator = new DeleteProductValidator(saleRepository);
    }

    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        // Validação do comando antes da execução
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Busca o produto no banco
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            // Retorna um resultado informando que o produto não foi encontrado, sem lançar exceção
            return new DeleteProductResult
            {
                Success = false,
                Message = "Produto não encontrado."
            };
        }

        // Deleta o produto
        await _productRepository.DeleteAsync(product);

        // Retorna um resultado indicando sucesso
        return new DeleteProductResult
        {
            Success = true,
            Message = "Produto deletado com sucesso.",
            ProductId = request.ProductId
        };
    }
}
