using System.Text;
using System.Text.Json;
using Bogus;
using Elastic.Clients.Elasticsearch;
using FluentAssertions;
using Library.Commands.Books.AddBook;
using Library.Commands.Books.BookSyncReadonly;
using Library.Domain.Domain;
using Library.IntegrationTests.Core;
using Library.Models.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.IntegrationTests.Tests.BookEndpoint;

public class CreateBookTests(LibraryApp app) : IClassFixture<LibraryApp>
{
    [Fact]
    public async Task CreateBook_ValidRequest_ReturnsCreated()
    {
        // Arrange
        var faker = new Faker();
        var request = new AddBookRequest(faker.Commerce.Ean13());

        var req = new HttpRequestMessage(HttpMethod.Post, "books");
        var json = JsonSerializer.Serialize(request,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        req.Content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await app.Api.Client.SendAsync(req);
        await app.Processor.Listener.WaitForProcessing<BookSyncReadonlyRequest>(default);

        // Assert
        response.EnsureSuccessStatusCode();

        var newBooks = await app.DbContext.Set<Book>().ToListAsync();
        newBooks.Should().HaveCount(1);
        newBooks[0].Isbn.Should().Be(request.Isbn);

        var elasticClient = app.Api.Services.GetRequiredService<ElasticsearchClient>();
        var bookModel = await elasticClient.GetAsync<BookViewModel>(newBooks[0].BookId);
    }
}