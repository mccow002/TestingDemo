namespace Aspire.Hosting;

public sealed class MockServerResource(string name) : ContainerResource(name), IResourceWithServiceDiscovery
{
    
}