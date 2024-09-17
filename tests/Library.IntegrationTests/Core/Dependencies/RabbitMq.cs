using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Library.IntegrationTests.Core.Dependencies;

public class RabbitMq
{
    public RabbitMq()
    {
        Container = new ContainerBuilder().WithImage("rabbitmq:3.13-management")
            .WithPortBinding(Port, 5672)
            .WithPortBinding(Random.Shared.Next(10000, 60000), 15672)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
            .Build();      
        
        ConnectionString = $"amqp://guest:guest@localhost:{Port}";;
    }
    
    public static int Port { get; } = Random.Shared.Next(10000, 60000);

    public IContainer Container { get; }

    public string ConnectionString { get; }
}