using Azure.Provisioning.ServiceBus;
using Library.Api.AppHost;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var useBooksMock = builder.Configuration.GetValue("mockBooks", false);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("Library");

var sb = builder.AddAzureServiceBus("service-bus")
    .ConfigureInfrastructure(cfg =>
    {
        var serviceBusNamespace = cfg.GetProvisionableResources()
            .OfType<ServiceBusNamespace>()
            .Single();

        serviceBusNamespace.Sku = new ServiceBusSku
        {
            Name = ServiceBusSkuName.Standard
        };
    });

var elastic = builder.AddElasticsearch("elasticsearch")
    .WithLifetime(ContainerLifetime.Persistent);

if (!builder.Configuration.GetValue("UseVolumes", true))
{
    sql.WithDataVolume();
    elastic.WithDataVolume();
}

var migration = builder.AddProject<Projects.Library_DataMigration>("setup")
    .WaitFor(db)
    .WithReference(db);

var api = builder.AddProject<Projects.Library_Api>("libraryapi")
    .WithReference(sb)
    .WithReference(db)
    .WithReference(elastic)
    .WaitFor(sb)
    .WaitForCompletion(migration)
    .WaitFor(elastic)
    .WithSyncReadonlyCommand();

api.WithUrlForEndpoint("https", ep => new()
{
    Url = "/Scalar",
    DisplayText = "Scalar",
    DisplayLocation = UrlDisplayLocation.SummaryAndDetails
});

var processor = builder.AddProject<Projects.Library_MessageProcessor>("library-message-processor")
    .WithReference(db)
    .WithReference(sb)
    .WithReference(api)
    .WithReference(elastic)
    .WaitFor(sb)
    .WaitForCompletion(migration)
    .WaitFor(api)
    .WaitFor(elastic);

var ui = builder.AddNpmApp("library-ui", "../library-ui")
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/")
    .WithReference(api)
    .PublishAsDockerFile();

api.WithEnvironment("Origin", ui.GetEndpoint("http"));

if (builder.ExecutionContext.IsRunMode)
{
    api.WithRoleAssignments(sb, ServiceBusBuiltInRole.AzureServiceBusDataSender);
    processor.WithRoleAssignments(sb, ServiceBusBuiltInRole.AzureServiceBusDataOwner);

    // var cli = builder.AddProject<Projects.Library_CLI>("library-cli")
    //     .WithReference(db)
    //     .WithReference(elastic)
    //     .WithReference("books-api", new Uri(builder.Configuration.GetConnectionString("GoogleBooks")))
    //     .WaitFor(elastic)
    //     .WaitForCompletion(migration)
    //     .WithExplicitStart()
    //     .WithSyncReadonlyCommand();
}

if (useBooksMock)
{
    var booksApiMock = builder
        .AddMockServer("books-api", "../../../../tests/Library.IntegrationTests/ApiSpecs/GoogleBooks/")
        .ExcludeFromManifest();
    
    api
        .WithReference(booksApiMock)
        .WaitFor(booksApiMock);

    processor
        .WithReference(booksApiMock)
        .WaitFor(booksApiMock);
}
else
{
    api.WithReference("books-api", new Uri(builder.Configuration.GetConnectionString("GoogleBooks")));
    processor.WithReference("books-api", new Uri(builder.Configuration.GetConnectionString("GoogleBooks")));
}

builder.Build().Run();
