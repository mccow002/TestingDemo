using PactNet;
using PactNet.Infrastructure.Outputters;
using PactNet.Output.Xunit;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace Library.ContractTests;

public class LibraryApiTests : IClassFixture<LibraryApiFactory>
{
    private readonly ITestOutputHelper _output;
    private readonly HttpClient _client;

    public LibraryApiTests(ITestOutputHelper output, LibraryApiFactory apiFactory)
    {
        _output = output;
        _client = apiFactory.CreateClient();
    }
    
    [Fact]
    public async Task ShouldReturnBook_WhenCatalogueLoaded()
    {
        // Arrange
        var config = new PactVerifierConfig
        {
            Outputters = new List<IOutput>
            {
                // NOTE: PactNet defaults to a ConsoleOutput, however
                // xUnit 2 does not capture the console output, so this
                // sample creates a custom xUnit outputter. You will
                // have to do the same in xUnit projects.
                new XunitOutput(_output),
            },
            LogLevel = PactLogLevel.Debug
        };
        
        var pactPath = Path.Combine("..", "..", "..", "..", "Library.Contract.ConsumerTests", "pacts", "Library API Consumer-Library API.json");
        
        // Act / Assert
        using var pactVerifier = new PactVerifier("Library API", config);
        
        pactVerifier
            .WithHttpEndpoint(new Uri("http://localhost:5000"))
            .WithFileSource(new FileInfo(pactPath))
            .WithProviderStateUrl(new Uri("http://localhost:5000/provider-states"))
            .Verify();
    }
}