using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Library.Commands.Books.BookSyncReadonly;
using Library.Domain.Domain;
using Library.Domain.Tests.Fakes;
using Library.GoogleBooks.Models;
using Library.Models.Contracts;
using NSubstitute;
using Soenneker.Utils.AutoBogus;

namespace Library.Commands.Tests.Books.BookSyncReadonly.Fixtures;

public class BookSyncReadonlyFixture : Fixture
{
    public IGoogleBooksApiClient MockApiClient { get; }
    public IBookRepository MockBookRepository { get; }
    public IReadonlyStore MockReadonlyStore { get; }

    public Book Book { get; set; }

    public SearchItem SearchItem { get; set; }

    public BookSyncReadonlyFixture()
    {
        Customize(new AutoNSubstituteCustomization());
        var bookFaker = new BookGenerator();
        var resultsFaker = new AutoFaker<SearchItem>();

        MockApiClient = this.Freeze<IGoogleBooksApiClient>();
        MockBookRepository = this.Freeze<IBookRepository>();
        MockReadonlyStore = this.Freeze<IReadonlyStore>();

        Book = bookFaker.Generate();
        SearchItem = resultsFaker.Generate();

        MockBookRepository.GetById(Arg.Any<Guid>(), default).Returns(_ => Book);
        MockApiClient.GetBookById(Arg.Any<string>(), Arg.Any<string>()).Returns(_ => SearchItem);
    }

    public BookSyncReadonlyHandler CreateSut()
    {
        return this.Create<BookSyncReadonlyHandler>();
    }
}