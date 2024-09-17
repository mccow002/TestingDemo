using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Library.Commands.Books.AddBook;
using Library.Domain.Contracts;

namespace Library.Commands.Tests.Books.AddBookHandlerTests.Fixtures;

public class AddBookHandlerFixture : Fixture
{
    public ILibraryRepository MockLibraryRepository { get; set; }
    
    public AddBookHandlerFixture()
    {
        Customize(new AutoNSubstituteCustomization());

        MockLibraryRepository = this.Freeze<ILibraryRepository>();
    }
    
    public AddBookHandler CreateSut()
    {
        return this.Create<AddBookHandler>();
    }
}