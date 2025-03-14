using Ambev.DeveloperEvaluation.WebApi;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

public class SaleFunctionalTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SaleFunctionalTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact(DisplayName = "Dada uma venda válida, ao criar, ela deve retornar sucesso")]
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

        var content = new StringContent(JsonConvert.SerializeObject(saleRequest), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/sales", content);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
