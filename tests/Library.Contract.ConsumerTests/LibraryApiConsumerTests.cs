using System.Net;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PactNet.Output.Xunit;
using Refit;
using Xunit.Abstractions;

namespace Library.ContractTests;

public class LibraryApiConsumerTests
{
    private readonly IPactBuilderV4 _pactBuilder;

    public LibraryApiConsumerTests(ITestOutputHelper output)
    {
        var pact = Pact.V4("Library API Consumer", "Library API", new PactConfig
        {
            Outputters = new List<IOutput>
            {
                new XunitOutput(output)
            },
            LogLevel = PactLogLevel.Debug
        });
        _pactBuilder = pact.WithHttpInteractions();
    }

    [Fact]
    public async Task Test1()
    {
        // Arrange
        _pactBuilder
            .UponReceiving("A GET Request to get all books")
            .Given("Books exist")
            .WithRequest(HttpMethod.Get, "/catalogue/books")
            .WillRespond()
            .WithHeader("Content-Type", "application/json; charset=utf-8")
            .WithStatus(HttpStatusCode.OK)
            .WithJsonBody(new[]
            {
                new
                {
                    bookId = "c928392a-46df-471c-2967-08dcecd495fc",
                    book = new
                    {
                        bookId = "c928392a-46df-471c-2967-08dcecd495fc",
                        authorName = "L. M. Sagas",
                        coverLink =
                            "http://books.google.com/books/publisher/content?id=jjG4EAAAQBAJ&printsec=frontcover&img=1&zoom=1&edge=curl&imgtk=AFLRE72WE_jN-bZwsW5RWWRDuzFKhiCIjIBHKIUtP_7IcnFhnRlODP7bZzQIGZrh67GHLmpA9zriEaK3RV9RbmaLyFsvl5P_0wR_DyDB3mtOkjsaNkQIRoIUExqC9loKjvqpQDCd6AIX&source=gbs_api",
                        publishDate = "2024-03-19",
                        description =
                            "<p><b>L. M. Sagas's debut, <i>Cascade Failure</i>, is a high-octane, sci-fi adventure blending J. S. Dewes's Divide series with <i>Firefly</i>. It features a fierce, messy, chaotic space family, vibrant worlds, and an exploration of the many ways to be—and not to be—human.<br><br></b><b>Most Anticipated Books of 2024—<i>Goodreads</i>, <i>Polygon</i>, <i>The Nerd Daily<br></i></b><b>Best SFF Novels of 2024 (so far)—<i>Bookpage</i><br></b><br>There are only three real powers in the Spiral: the corporate power of the Trust versus the Union's labor's leverage. Between them the Guild tries to keep everyone's hands above the table. It ain't easy.<br><br>Branded a Guild deserter, Jal \"accidentally\" lands a ride on a Guild ship. Helmed by an AI, with a ship's engineer/medic who doesn't see much of a difference between the two jobs, and a \"don't make me shoot you\" XO, the Guild crew of the <i>Ambit</i> is a little . . . different.<br><br>They're also in over their heads. Responding to a distress call from an abandoned planet, they find a mass grave, and a live programmer who knows how it happened. The Trust has plans. This isn't the first dead planet, and it's not going to be the last.<br><br>Unless the crew of the <i>Ambit</i> can stop it.<br><br>Ambit's Run series<br><i>Cascade Failure</i><br><br>At the Publisher's request, this title is being sold without Digital Rights Management Software (DRM) applied.</p>",
                        volumeId = "jjG4EAAAQBAJ",
                        publisher = "Tor Publishing Group",
                        title = "Cascade Failure",
                        subject = "Fiction / Science Fiction / Action & Adventure"
                    }
                }
            });
        
        // Act
        await _pactBuilder.VerifyAsync(async ctx =>
        {
            var client = RestService.For<ILibraryApiClient>(ctx.MockServerUri.ToString());
            var result = await client.GetCatalogue();
            
            Assert.Equal(result[0].BookId, Guid.Parse("c928392a-46df-471c-2967-08dcecd495fc"));
        });
    }
}