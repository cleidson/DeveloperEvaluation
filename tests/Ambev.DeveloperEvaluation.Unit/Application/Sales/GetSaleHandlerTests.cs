using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetSaleHandlerTests
{



    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly GetSaleHandler _handler;

    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_saleRepository,_mapper);
    }

    [Fact(DisplayName = "Given a valid sale ID When retrieving Then returns sale details")]
    public async Task Handle_ValidRequest_ReturnsSale()
    {
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId };

        _saleRepository.GetByIdAsync(saleId).Returns(sale);

        var result = await _handler.Handle(new GetSaleQuery { SaleId = saleId }, CancellationToken.None);

        result.Should().NotBeNull();
        result.BranchId.Should().Be(sale.Id);
    }
}
