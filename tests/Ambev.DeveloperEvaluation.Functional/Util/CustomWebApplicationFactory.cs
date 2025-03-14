using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ambev.DeveloperEvaluation.Domain.Repositories; 
using NSubstitute;
using System;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureServices(services =>
        {
            // Remover repositórios reais se necessário
            services.RemoveAll(typeof(ISaleRepository));
            services.RemoveAll(typeof(IBranchRepository));
            services.RemoveAll(typeof(IProductRepository));

            // Mockar repositórios para testes
            var saleRepositoryMock = Substitute.For<ISaleRepository>();
            var branchRepositoryMock = Substitute.For<IBranchRepository>();
            var productRepositoryMock = Substitute.For<IProductRepository>();

            services.AddSingleton(saleRepositoryMock);
            services.AddSingleton(branchRepositoryMock);
            services.AddSingleton(productRepositoryMock);
        });
    }
}
