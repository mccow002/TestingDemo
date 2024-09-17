using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Library.Commands.Books.BookSyncReadonly;
using Library.Domain.Domain;
using Library.Domain.Tests.Fakes;
using Library.Models.Contracts;
using Library.OpenLibraryApi.Models;
using NSubstitute;
using Soenneker.Utils.AutoBogus;

namespace Library.Commands.Tests.Books.BookSyncReadonly.Fixtures;

public class BookSyncReadonlyFixture : Fixture
{
    public IOpenLibraryApiClient MockApiClient { get; }
    public IBookRepository MockBookRepository { get; }
    public IReadonlyStore MockReadonlyStore { get; }

    public Book Book { get; set; }

    public SearchResults SearchResults { get; set; }

    public BookSyncReadonlyFixture()
    {
        Customize(new AutoNSubstituteCustomization());
        var bookFaker = new BookGenerator();
        var resultsFaker = new AutoFaker<SearchResults>();

        MockApiClient = this.Freeze<IOpenLibraryApiClient>();
        MockBookRepository = this.Freeze<IBookRepository>();
        MockReadonlyStore = this.Freeze<IReadonlyStore>();

        Book = bookFaker.Generate();
        SearchResults = resultsFaker.Generate();

        MockBookRepository.GetById(Arg.Any<Guid>(), default).Returns(_ => Book);
        MockApiClient.GetBookByIsbn(Arg.Any<string>()).Returns(_ => SearchResults);
    }

    public BookSyncReadonlyHandler CreateSut()
    {
        return this.Create<BookSyncReadonlyHandler>();
    }
}