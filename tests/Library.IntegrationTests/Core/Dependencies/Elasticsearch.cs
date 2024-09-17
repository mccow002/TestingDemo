using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Library.IntegrationTests.Core.Dependencies;

public class Elasticsearch
{
    public Elasticsearch()
    {
        Container = new ContainerBuilder().WithImage("elasticsearch:8.15.1")
            .WithPortBinding(Port, 9200)
            .WithPortBinding(Random.Shared.Next(10000, 60000), 9300)
            .WithEnvironment("discovery.type", "single-node")
            .WithEnvironment("xpack.security.enabled", "false")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(9200))
            .Build();      
        
        ConnectionString = $"http://localhost:{Port}";
    }
    
    public static int Port { get; } = Random.Shared.Next(10000, 60000);

    public IContainer Container { get; }

    public string ConnectionString { get; }
}