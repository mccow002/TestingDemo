using AutoFixture;
using Library.Commands.Books.BookSyncReadonly;
using Library.Commands.Tests.Books.BookSyncReadonly.Fixtures;
using Library.Domain.Domain;
using Library.Models.Books;
using NSubstitute;

namespace Library.Commands.Tests.Books.BookSyncReadonly;

public class BookSyncReadonlyTests
{
    private BookSyncReadonlyFixture _fixture;

    public BookSyncReadonlyTests()
    {
        _fixture = new BookSyncReadonlyFixture();
    }
    
    [Fact]
    public async Task Handle_WhenCalled_ShouldSyncBook()
    {
        // Arrange
        var sut = _fixture.CreateSut();
        var request = new BookSyncReadonlyRequest(_fixture.Create<Guid>());
        
        // Act
        await sut.Handle(request, default);
        
        // Assert
        _fixture.MockBookRepository.Received().GetById(request.BookId, default);
        _fixture.MockApiClient.Received().GetBookById(_fixture.Book.VolumeId, default);

        _fixture.MockReadonlyStore.Received().Index(Arg.Any<BookViewModel>(), default);
    }
}