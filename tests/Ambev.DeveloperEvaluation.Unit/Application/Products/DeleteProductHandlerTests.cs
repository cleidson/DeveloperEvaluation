using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class DeleteProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly DeleteProductHandler _handler;

    public DeleteProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new DeleteProductHandler(_productRepository, _saleRepository);
    }

    [Fact(DisplayName = "Given a valid product When deleting Then deletes successfully")]
    public async Task Handle_ValidDelete_Success()
    {
        var command = new DeleteProductCommand(Guid.NewGuid());
        _productRepository.GetByIdAsync(command.ProductId).Returns(new Product());

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().BeTrue();
        await _productRepository.Received(1).DeleteAsync(Arg.Any<Product>());
    }
}
