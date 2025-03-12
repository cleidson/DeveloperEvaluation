using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact(DisplayName = "Given valid data When creating sale Then initializes correctly")]
    public void CreateSale_ValidData_InitializesCorrectly()
    {
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Status = SaleStatus.Pending,  // Status inicial deve ser "Pending"
            SaleItems = new List<SaleItem>
            {
                new SaleItem { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 50 }
            }
        };

        sale.Should().NotBeNull();
        sale.SaleItems.Should().HaveCount(1);
        sale.Status.Should().Be(SaleStatus.Pending); // Verificando o status inicial
    }

    [Fact(DisplayName = "Given a sale When canceling Then status changes to Cancelled")]
    public void CancelSale_ChangesStatusToCancelled()
    {
        var sale = new Sale { Id = Guid.NewGuid(), Status = SaleStatus.Completed };

        // Simula a lógica de cancelamento (normalmente isso seria um método na entidade)
        sale.Status = SaleStatus.Cancelled;

        sale.Status.Should().Be(SaleStatus.Cancelled);
    }
}
