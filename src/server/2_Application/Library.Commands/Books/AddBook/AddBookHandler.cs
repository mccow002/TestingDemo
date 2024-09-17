using Library.Domain.Contracts;
using Library.Domain.Domain;
using MediatR;

namespace Library.Commands.Books.AddBook;

public record AddBookRequest(string Isbn) : IRequest;

public class AddBookHandler(ILibraryRepository repository) : IRequestHandler<AddBookRequest>
{
    public async Task Handle(AddBookRequest request, CancellationToken cancellationToken)
    {
        var book = Book.Create(request.Isbn);
        repository.Add(book);
        await repository.SaveChangesAsync(cancellationToken);
    }
}