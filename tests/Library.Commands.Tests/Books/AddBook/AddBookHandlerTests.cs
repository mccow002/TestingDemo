using AutoFixture;
using Library.Commands.Books.AddBook;
using Library.Commands.Tests.Books.AddBookHandlerTests.Fixtures;
using Library.Domain.Domain;
using Library.Domain.DomainEvents.Books;
using NSubstitute;

namespace Library.Commands.Tests.Books.AddBookHandlerTests;

public class AddBookHandlerTests
{
    private AddBookHandlerFixture _fixture;
    
    public AddBookHandlerTests()
    {
        _fixture = new AddBookHandlerFixture();
    }
    
    [Fact]
    public async Task Handle_WhenCalled_ShouldAddBook()
    {
        // Arrange
        var sut = _fixture.CreateSut();
        var request = new AddBookRequest(_fixture.Create<string>());
        
        // Act
        await sut.Handle(request, default);
        
        // Assert
        _fixture.MockLibraryRepository.Received().Add(
            Arg.Is<Book>(b => 
                b.Isbn == request.Isbn &&
                b.DomainEvents.Count > 0 &&
                b.DomainEvents[0] is BookCreatedEvent
            )
        );
        await _fixture.MockLibraryRepository.Received().SaveChangesAsync(default);
    }
}