using Aspire.Hosting;

namespace Aspire.Hosting;

public static class MockServerResourceBuilderExtensions
{
    public static IResourceBuilder<MockServerResource> AddMockServer(
        this IDistributedApplicationBuilder builder,
        string name,
        string expectationsFolderPath)
    {
        var resource = new MockServerResource(name);

        return builder.AddResource(resource)
            .WithImage("mockserver/mockserver")
            .WithImageRegistry("docker.io")
            .WithHttpEndpoint(targetPort: 1080)
            .WithBindMount(expectationsFolderPath, "/config")
            .WithEnvironment("MOCKSERVER_INITIALIZATION_JSON_PATH", "/config/*.json")
            .WithEnvironment("MOCKSERVER_WATCH_INITIALIZATION_JSON", "true")
            .WithHttpHealthCheck("/mockserver/dashboard")
            .WithUrlForEndpoint("http", e =>
            {
                e.DisplayText = "Dashboard";
                e.Url = $"{e.Endpoint.Url}/mockserver/dashboard";
            });
    }
}