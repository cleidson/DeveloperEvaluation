using Ambev.DeveloperEvaluation.Integration.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Sales;

public class GetSalesIntegrationTests : IntegrationTestBase
{
    [Fact(DisplayName = "Dado que há vendas registradas, quando buscar todas, então deve retornar a lista.")]
    public async Task GetSales_ShouldReturnListOfSales()
    {
        var response = await Client.GetAsync("/api/sales");
        response.EnsureSuccessStatusCode();
    }
}
