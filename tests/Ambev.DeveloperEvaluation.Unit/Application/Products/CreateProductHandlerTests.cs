using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="CreateProductHandlerTests"/> class.
/// </summary>
public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new CreateProductHandler(_productRepository);
    }

    [Fact(DisplayName = "Given valid product data When creating Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        var command = new CreateProductCommand { Name = "Produto Teste", Price = 150 };
        var product = new Product { Id = Guid.NewGuid(), Name = command.Name, Price = command.Price };

        _productRepository.AddAsync(Arg.Any<Product>()).Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
        await _productRepository.Received(1).AddAsync(Arg.Any<Product>());
    }
}
