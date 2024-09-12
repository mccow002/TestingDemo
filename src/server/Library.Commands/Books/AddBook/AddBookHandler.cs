using MediatR;

namespace Library.Commands.Books.AddBook;

public record AddBookRequest(string Isbn) : IRequest;

public class AddBookHandler : IRequestHandler<AddBookRequest>
{
    public Task Handle(AddBookRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}