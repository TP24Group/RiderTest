using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RiderTestFailures.Configuration;

namespace Tests;

public class HttpTestServer : IDisposable
{
    private TestServer? activeServer;
    private HttpClient? activeHttpClient;
    
    public async Task<HttpClient> GetClient() => this.activeHttpClient ??= await this.CreateHttpClient();
    public async Task<TestServer> GetServer() => this.activeServer ??= await this.CreateServer();
    
    private async Task<HttpClient> CreateHttpClient()
    {
        var server = await this.GetServer();
        var client = server.CreateClient();

        return client;
    }
    
    private async Task<TestServer> CreateServer()
    {
        var applicationBuilder = WebApplication.CreateBuilder();
        applicationBuilder.Logging.ClearProviders();
        applicationBuilder.WebHost.UseTestServer();
        applicationBuilder.Environment.EnvironmentName = "Test";
        applicationBuilder.Services.AddApiServices();

        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB", false);

        var app = applicationBuilder.Build();
        app.ConfigureApi();

        await app.StartAsync();

        var testServer = (TestServer)app.Services.GetRequiredService<IServer>();
        return testServer;
    }
    
    public void Dispose()
    {
        this.activeHttpClient?.Dispose();
        this.activeServer?.Dispose();
    }
}