extern alias api;
using Library.IntegrationTests.Core.Dependencies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using ApiProgram=api::Program;

namespace Library.IntegrationTests.Core;

public class LibraryApiFactory : WebApplicationFactory<ApiProgram>
{
    private readonly SqlServer _sqlServer;
    private readonly Dependencies.Elasticsearch _elasticsearch;
    private readonly OpenLibraryMockApi _openLibraryApi;
    private readonly RabbitMq _rabbitMq;

    public LibraryApiFactory(
        SqlServer sqlServer, 
        Dependencies.Elasticsearch elasticsearch, 
        OpenLibraryMockApi openLibraryApi,
        RabbitMq rabbitMq)
    {
        _sqlServer = sqlServer;
        _elasticsearch = elasticsearch;
        _openLibraryApi = openLibraryApi;
        _rabbitMq = rabbitMq;
    }

    public HttpClient Client { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureAppConfiguration(config =>
        {
            var settings = new Dictionary<string, string>
            {
                {
                    "ConnectionStrings:DbConn",
                    _sqlServer.ConnectionString
                },
                {
                    "RabbitMq:Uri",
                    _rabbitMq.ConnectionString
                },
                {
                    "Elasticsearch:Uri",
                    _elasticsearch.ConnectionString
                },
                {
                    "OpenLibraryApi:Uri",
                    _openLibraryApi.Url
                }
            };

            config.AddInMemoryCollection(settings);
        });
        
        base.ConfigureWebHost(builder);
    }

    public async Task Start()
    {
        Client = CreateClient();
    }
}