using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Library.IntegrationTests.Core.Dependencies;

public class GoogleBooksMockApi
{
    private readonly int _port = Random.Shared.Next(10000, 60000);
    
    private readonly RefitSettings _refitSettings = new()
    {
        ContentSerializer = new NewtonsoftJsonContentSerializer(
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() })
    };

    public GoogleBooksMockApi()
    {
        Container = new ContainerBuilder().WithImage("mockserver/mockserver")
            .WithPortBinding(_port, 1080)
            .WithBindMount(Path.Combine(Environment.CurrentDirectory, $"ApiSpecs/GoogleBooks"), "/config")
            .WithEnvironment("MOCKSERVER_INITIALIZATION_JSON_PATH", $"/config/*.json")
            //.WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(_port))
            .Build();
        
        

        Url = $"http://localhost:{_port}";
        Client = RestService.For<IMockServerClient>(Url, _refitSettings);
    }
    
    public IContainer Container { get; }

    public string Url { get; }
    
    public IMockServerClient Client { get; }
}