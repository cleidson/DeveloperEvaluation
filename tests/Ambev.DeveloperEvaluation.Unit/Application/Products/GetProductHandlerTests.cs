using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
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

public class GetProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly GetProductHandler _handler;

    public GetProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new GetProductHandler(_productRepository);
    }

    [Fact(DisplayName = "Given a valid product ID When retrieving Then returns product details")]
    public async Task Handle_ValidRequest_ReturnsProduct()
    {
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId, Name = "Produto Teste", Price = 150 };

        _productRepository.GetByIdAsync(productId).Returns(product);

        var result = await _handler.Handle(new GetProductQuery { ProductId = productId }, CancellationToken.None);

        result.Should().NotBeNull();
        result.ProductId.Should().Be(product.Id);
        result.Name.Should().Be("Produto Teste");
    }
}