using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Library.IntegrationTests.Core.Dependencies;

public class SqlServer
{
    public SqlServer()
    {
        Container = new ContainerBuilder().WithImage("mcr.microsoft.com/mssql/server")
            .WithPortBinding(SqlPort, 1433)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("SA_PASSWORD", "Test123!")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("Service Broker manager has started"))
            .Build();      
        
        ConnectionString = $"Server=localhost,{SqlPort};Database=Library;User Id=sa;Password=Test123!;TrustServerCertificate=True;";
    }
    
    public static int SqlPort { get; } = Random.Shared.Next(10000, 60000);

    public IContainer Container { get; }

    public string ConnectionString { get; }
}