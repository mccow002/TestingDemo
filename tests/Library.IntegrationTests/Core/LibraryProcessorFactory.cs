extern alias processor;
using Library.IntegrationTests.Core.Dependencies;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProcessorProgram = processor::Program;

namespace Library.IntegrationTests.Core;

public class LibraryProcessorFactory : WebApplicationFactory<ProcessorProgram>
{
    private readonly SqlServer _sqlServer;
    private readonly Dependencies.Elasticsearch _elasticsearch;
    private readonly GoogleBooksMockApi _googleBooksApi;
    private readonly RabbitMq _rabbitMq;

    private IHost _processor;

    public LibraryProcessorFactory(
        SqlServer sqlServer,
        Dependencies.Elasticsearch elasticsearch,
        GoogleBooksMockApi googleBooksApi,
        RabbitMq rabbitMq)
    {
        _sqlServer = sqlServer;
        _elasticsearch = elasticsearch;
        _googleBooksApi = googleBooksApi;
        _rabbitMq = rabbitMq;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.Configure(_ => { });
        base.ConfigureWebHost(builder);
    }

    protected override IHost CreateHost(IHostBuilder builder)
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
                    "GoogleBooksApi:Uri",
                    _googleBooksApi.Url
                }
            };

            config.AddInMemoryCollection(settings);
        });

        builder.ConfigureServices(services =>
        {
            services.AddSingleton<EventListener>();
            services.AddConsumeObserver<ProcessorObserver>();
        });

        return base.CreateHost(builder);
    }

    public EventListener Listener { get; private set; }

    public async Task Start()
    {
        _processor = Services.GetRequiredService<IHost>();
        await _processor.StartAsync();
        
        Listener = Services.GetRequiredService<EventListener>();
    }

    public async Task Stop()
    {
        await _processor.StopAsync();
    }
}