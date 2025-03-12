using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleCommand> createSaleFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.BranchId, f => f.Random.Guid())
        .RuleFor(s => s.SaleItems, f => new List<SaleItem>
        {
            new SaleItem
            {
                ProductId = f.Random.Guid(),
                Quantity = f.Random.Int(1, 20),
                UnitPrice = f.Finance.Amount(10, 100)
            }
        });

    public static CreateSaleCommand GenerateValidCommand()
    {
        return createSaleFaker.Generate();
    }
}