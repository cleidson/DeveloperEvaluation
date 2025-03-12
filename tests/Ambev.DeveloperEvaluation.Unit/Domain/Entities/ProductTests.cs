using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class ProductTests
{
    [Fact(DisplayName = "Given valid data When creating product Then initializes correctly")]
    public void CreateProduct_ValidData_InitializesCorrectly()
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Produto Teste",
            Price = 100.50m
        };

        product.Should().NotBeNull();
        product.Name.Should().Be("Produto Teste");
        product.Price.Should().Be(100.50m);
    }

    [Fact(DisplayName = "Given a product When updating details Then changes values correctly")]
    public void UpdateProduct_ChangesValues()
    {
        var product = new Product { Id = Guid.NewGuid(), Name = "Old Product", Price = 50 };

        product.Name = "Updated Product";
        product.Price = 75;

        product.Name.Should().Be("Updated Product");
        product.Price.Should().Be(75);
    }
}
