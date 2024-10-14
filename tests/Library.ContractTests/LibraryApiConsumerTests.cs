using System.Net;
using PactNet;

namespace Library.ContractTests;

public class LibraryApiConsumerTests
{
    private readonly IPactBuilderV4 _pactBuilder;
    
    public LibraryApiConsumerTests()
    {
        var pact = Pact.V4("Library API Consumer", "Library API", new PactConfig());
        _pactBuilder = pact.WithHttpInteractions();
    }
    
    [Fact]
    public void Test1()
    {
        // Arrange
        _pactBuilder
            .UponReceiving("A GET Request to get all books")
            .WithRequest(HttpMethod.Get, "/catalogue/books")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithJsonBody(new
            {

            });
    }
}