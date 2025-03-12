using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CancelSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly CancelSaleHandler _handler;

    public CancelSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new CancelSaleHandler(_saleRepository);
    }

    [Fact(DisplayName = "Given a valid sale When canceling Then updates status to cancelled")]
    public async Task Handle_ValidCancel_Success()
    {
        var command = new CancelSaleCommand { SaleId = Guid.NewGuid() };
        var sale = new Sale { Id = command.SaleId, Status = SaleStatus.Cancelled };

        _saleRepository.GetByIdAsync(command.SaleId).Returns(sale);
        _saleRepository.UpdateAsync(Arg.Any<Sale>()).Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().BeTrue();
        sale.Status.Should().Equals(SaleStatus.Cancelled);
        await _saleRepository.Received(1).UpdateAsync(sale);
    }
}
