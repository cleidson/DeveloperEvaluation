using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class DeleteProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IValidator<DeleteProductCommand> _validator;
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _saleRepository = Substitute.For<ISaleRepository>();
            _validator = Substitute.For<IValidator<DeleteProductCommand>>();
            _handler = new DeleteProductHandler(_productRepository, _saleRepository, _validator);
        }

        [Fact(DisplayName = "Given a valid product When deleting Then deletes successfully")]
        public async Task Handle_ValidDelete_Success()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var command = new DeleteProductCommand { ProductId = productId };

            _validator.ValidateAsync(command, CancellationToken.None).Returns(new ValidationResult()); // Validação bem-sucedida
            _productRepository.GetByIdAsync(productId).Returns(new Product { Id = productId });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Produto deletado com sucesso.");
            result.ProductId.Should().Be(productId);
            await _productRepository.Received(1).DeleteAsync(Arg.Any<Product>());
        }

        [Fact(DisplayName = "Given a non-existing product When deleting Then returns error")]
        public async Task Handle_ProductNotFound_Failure()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var command = new DeleteProductCommand { ProductId = productId };

            _validator.ValidateAsync(command, CancellationToken.None).Returns(new ValidationResult());
            _productRepository.GetByIdAsync(productId).Returns((Product)null); // Produto não encontrado

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Produto não encontrado.");
            await _productRepository.DidNotReceive().DeleteAsync(Arg.Any<Product>());
        }

        [Fact(DisplayName = "Given an invalid request When deleting Then returns validation error")]
        public async Task Handle_InvalidRequest_ValidationFailure()
        {
            // Arrange
            var command = new DeleteProductCommand { ProductId = Guid.Empty }; // ID inválido
            var validationErrors = new ValidationResult(new[]
            {
                new ValidationFailure("ProductId", "O ID do produto é inválido.")
            });

            _validator.ValidateAsync(command, CancellationToken.None).Returns(validationErrors);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Requisição inválida:");
            await _productRepository.DidNotReceive().DeleteAsync(Arg.Any<Product>());
        }
    }
}
