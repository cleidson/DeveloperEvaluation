using Ambev.DeveloperEvaluation.Integration.Configuration;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Sales;

public class CreateSaleIntegrationTests : IntegrationTestBase
{
    [Fact(DisplayName = "Dado uma venda válida, quando enviada à API, então deve ser criada com sucesso.")]
    public async Task CreateSale_ValidData_ShouldReturnSuccess()
    {
        var saleRequest = new
        {
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            SaleItems = new[]
            {
                new { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 100 }
            }
        };

        var response = await Client.PostAsJsonAsync("/api/sales", saleRequest);

        response.EnsureSuccessStatusCode(); // Falha se não for 200 OK ou 201 Created
    }
}
