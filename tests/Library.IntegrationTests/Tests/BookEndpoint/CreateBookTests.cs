using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;
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
using System.Text;
using System.Text.Json;
using Library.Repository;

namespace Library.IntegrationTests.Tests.BookEndpoint;

public class CreateBookTests //: IClassFixture<LibraryApp>
{
    //[Fact]
    //public async Task CreateBook_ValidRequest_ReturnsCreated()
    //{
    //    // Arrange
    //    var faker = new Faker();
    //    var request = new AddBookRequest(faker.Commerce.Ean13());

    //    var req = new HttpRequestMessage(HttpMethod.Post, "books");
    //    var json = JsonSerializer.Serialize(request,
    //        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    //    req.Content = new StringContent(json, Encoding.UTF8, "application/json");

    //    // Act
    //    var response = await app.Api.Client.SendAsync(req);
    //    await app.Processor.Listener.WaitForProcessing<BookSyncReadonlyRequest>(default);

    //    // Assert
    //    response.EnsureSuccessStatusCode();

    //    await using var context = app.GetDbContext();
    //    var newBooks = await context.Set<Book>().ToListAsync();
    //    newBooks.Should().HaveCount(1);
    //    newBooks[0].VolumeId.Should().Be(request.Isbn);

    //    var elasticClient = app.Api.Services.GetRequiredService<ElasticsearchClient>();
    //    var bookModel = await elasticClient.GetAsync<BookViewModel>(newBooks[0].BookId);
    //}

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

        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.Library_Api_AppHost>(["UseVolumes=false"]);

        appHost.Services.ConfigureHttpClientDefaults(cb => cb.AddStandardResilienceHandler());

        await using var app = await appHost.BuildAsync();

        var resourceNotificationService = app.Services
            .GetRequiredService<ResourceNotificationService>();

        await app.StartAsync();

        await resourceNotificationService.WaitForResourceAsync("libraryapi", KnownResourceStates.Running);
        var httpClient = app.CreateHttpClient("libraryapi");

        // Act
        var response = await httpClient.SendAsync(req);
        //await app.Processor.Listener.WaitForProcessing<BookSyncReadonlyRequest>(default);

        // Assert
        response.EnsureSuccessStatusCode();

        var dbConn = await app.GetConnectionStringAsync("Library");
        var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
        optionsBuilder.UseSqlServer(dbConn);
        await using var context = new LibraryContext(optionsBuilder.Options);
        var newBooks = await context.Set<Book>().ToListAsync();
        newBooks.Should().HaveCount(1);
        newBooks[0].VolumeId.Should().Be(request.Isbn);

        //var elasticClient = app.Api.Services.GetRequiredService<ElasticsearchClient>();
        //var bookModel = await elasticClient.GetAsync<BookViewModel>(newBooks[0].BookId);
    }
}