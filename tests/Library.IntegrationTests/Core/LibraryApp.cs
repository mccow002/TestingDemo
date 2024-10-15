using Library.IntegrationTests.Core.Dependencies;
using Library.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Library.IntegrationTests.Core;

public class LibraryApp : IAsyncLifetime
{
    private readonly SqlServer _sqlServer;
    private readonly Dependencies.Elasticsearch _elasticsearch;
    private readonly GoogleBooksMockApi _googleBooksApi;
    private readonly RabbitMq _rabbitMq;
    
    public LibraryApp()
    {
        _sqlServer = new SqlServer();
        _elasticsearch = new Dependencies.Elasticsearch();
        _googleBooksApi = new GoogleBooksMockApi();
        _rabbitMq = new RabbitMq();
        
        Api = new LibraryApiFactory(_sqlServer, _elasticsearch, _googleBooksApi, _rabbitMq);
        Processor = new LibraryProcessorFactory(_sqlServer, _elasticsearch, _googleBooksApi, _rabbitMq);
    }

    public LibraryApiFactory Api { get;  }

    public LibraryProcessorFactory Processor { get;  }

    public LibraryContext GetDbContext()
    {
        var scope = Api.Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<LibraryContext>();
    }
    
    public async Task InitializeAsync()
    {
        await _sqlServer.Container.StartAsync();
        await _elasticsearch.Container.StartAsync();
        await _googleBooksApi.Container.StartAsync();
        await _rabbitMq.Container.StartAsync();
        
        await Api.Start();
        await Processor.Start();

        await using var context = GetDbContext();
        await DbInitializer.Initialize(context);
    }

    public async Task DisposeAsync()
    {
        await _sqlServer.Container.StopAsync();
        await _elasticsearch.Container.StopAsync();
        await _googleBooksApi.Container.StopAsync();
        await _rabbitMq.Container.StopAsync();
        
        await Api.DisposeAsync();
        await Processor.Stop();
    }
}