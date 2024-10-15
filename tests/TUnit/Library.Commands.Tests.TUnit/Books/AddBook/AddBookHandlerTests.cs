using AutoFixture;
using Library.Commands.Books.AddBook;
using Library.Commands.Tests.TUnit.Books.AddBook.Fixtures;
using Library.Domain.Domain;
using Library.Domain.DomainEvents.Books;
using NSubstitute;

namespace Library.Commands.Tests.TUnit.Books.AddBook;

public class AddBookHandlerTests
{
    private AddBookHandlerFixture _fixture;
    
    [Before(Test)]
    public async Task SetupTest(TestContext context)
    {
        _fixture = new AddBookHandlerFixture();
    }
    
    [Test]
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
                b.VolumeId == request.Isbn &&
                b.DomainEvents.Count > 0 &&
                b.DomainEvents[0] is BookCreatedEvent
            )
        );
        await _fixture.MockLibraryRepository.Received().SaveChangesAsync(default);
    }
}