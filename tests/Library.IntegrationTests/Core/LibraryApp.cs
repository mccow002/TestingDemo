using Library.IntegrationTests.Core.Dependencies;
using Library.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Library.IntegrationTests.Core;

public class LibraryApp : IAsyncLifetime
{
    private readonly SqlServer _sqlServer;
    private readonly Dependencies.Elasticsearch _elasticsearch;
    private readonly OpenLibraryMockApi _openLibraryApi;
    private readonly RabbitMq _rabbitMq;

    private IServiceScope? _serviceScope;
    
    public LibraryApp()
    {
        _sqlServer = new SqlServer();
        _elasticsearch = new Dependencies.Elasticsearch();
        _openLibraryApi = new OpenLibraryMockApi();
        _rabbitMq = new RabbitMq();
        
        Api = new LibraryApiFactory(_sqlServer, _elasticsearch, _openLibraryApi, _rabbitMq);
        Processor = new LibraryProcessorFactory(_sqlServer, _elasticsearch, _openLibraryApi, _rabbitMq);
    }

    public LibraryApiFactory Api { get;  }

    public LibraryProcessorFactory Processor { get;  }

    public LibraryContext DbContext { get; set; }
    
    public async Task InitializeAsync()
    {
        await _sqlServer.Container.StartAsync();
        await _elasticsearch.Container.StartAsync();
        await _openLibraryApi.Container.StartAsync();
        await _rabbitMq.Container.StartAsync();
        
        await Api.Start();
        await Processor.Start();

        _serviceScope = Api.Services.CreateScope();
        DbContext = _serviceScope.ServiceProvider.GetRequiredService<LibraryContext>();
        await DbInitializer.Initialize(DbContext);
    }

    public async Task DisposeAsync()
    {
        await _sqlServer.Container.StopAsync();
        await _elasticsearch.Container.StopAsync();
        await _openLibraryApi.Container.StopAsync();
        await _rabbitMq.Container.StopAsync();
        
        await Api.DisposeAsync();
        await Processor.Stop();
        
        _serviceScope?.Dispose();
    }
}