using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;

namespace Ambev.DeveloperEvaluation.Integration.Configuration;

public abstract class IntegrationTestBase : IDisposable
{
    protected readonly HttpClient Client;
    private readonly WebApplicationFactory<Program> _factory;

    protected IntegrationTestBase()
    {
        _factory = new WebApplicationFactory<Program>();
        Client = _factory.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
    }
}
