using PactNet;
using PactNet.Infrastructure.Outputters;
using PactNet.Output.Xunit;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace Library.ContractTests;

public class LibraryApiTests
{
    private readonly ITestOutputHelper _output;

    public LibraryApiTests(ITestOutputHelper output)
    {
        _output = output;
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
            .WithHttpEndpoint(new Uri("https://localhost:44314"))
            .WithFileSource(new FileInfo(pactPath))
            .WithProviderStateUrl(new Uri("https://localhost:44314/provider-states"))
            .Verify();
    }
}