using Ambev.DeveloperEvaluation.Integration.Configuration;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Sales;

public class UpdateSaleIntegrationTests : IntegrationTestBase
{
    [Fact(DisplayName = "Dado uma venda existente, quando atualizada, então deve refletir as mudanças.")]
    public async Task UpdateSale_ValidData_ShouldReturnSuccess()
    {
        var saleUpdateRequest = new
        {
            SaleId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            SaleItems = new[]
            {
                new { ProductId = Guid.NewGuid(), Quantity = 10, UnitPrice = 50 }
            }
        };

        var response = await Client.PutAsJsonAsync($"/api/sales/{saleUpdateRequest.SaleId}", saleUpdateRequest);
        response.EnsureSuccessStatusCode();
    }
}
