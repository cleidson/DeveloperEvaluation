using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new UpdateSaleHandler(_saleRepository);
    }

    [Fact(DisplayName = "Given a valid sale update When handling Then updates sale successfully")]
    public async Task Handle_ValidUpdate_Success()
    {
        var command = new UpdateSaleCommand { SaleId = Guid.NewGuid(), TotalAmount = 500 };
        var sale = new Sale { Id = command.SaleId, TotalAmount = 300 };

        _saleRepository.GetByIdAsync(command.SaleId).Returns(sale);
        _saleRepository.UpdateAsync(Arg.Any<Sale>()).Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().BeTrue();
        sale.TotalAmount.Should().Be(command.TotalAmount);
        await _saleRepository.Received(1).UpdateAsync(sale);
    }
}