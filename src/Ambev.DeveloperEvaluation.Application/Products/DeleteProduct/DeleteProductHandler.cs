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
    private readonly ISaleRepository _saleRepository;
    private readonly IValidator<DeleteProductCommand> _validator;

    public DeleteProductHandler(IProductRepository productRepository, ISaleRepository saleRepository, IValidator<DeleteProductCommand> validator)
    {
        _productRepository = productRepository;
        _saleRepository = saleRepository;
        _validator = validator;
    }

    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        // Validação do comando
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new DeleteProductResult
            {
                Success = false,
                Message = "Requisição inválida: " + string.Join("; ", validationResult.Errors)
            };
        }

        // Buscar o produto
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            return new DeleteProductResult
            {
                Success = false,
                Message = "Produto não encontrado."
            };
        }

        // Excluir o produto
        await _productRepository.DeleteAsync(product);

        return new DeleteProductResult
        {
            Success = true,
            Message = "Produto deletado com sucesso."
        };
    }
}
