using CRA.Domain.Mappers;
using Library.Models.Contracts;
using Library.OpenLibraryApi.Models;
using MediatR;

namespace Library.Commands.Books.BookSyncReadonly;

public record BookSyncReadonlyRequest(Guid BookId) : IRequest;

public class BookSyncReadonlyHandler(
    IOpenLibraryApiClient api, 
    IBookRepository bookRepository,
    IReadonlyStore readonlyStore) : IRequestHandler<BookSyncReadonlyRequest>
{
    public async Task Handle(BookSyncReadonlyRequest request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetById(request.BookId, cancellationToken);
        var results = await api.GetBookByIsbn(book.Isbn);
        var bookDetails = results.Docs.FirstOrDefault();
        
        var viewModel = bookDetails.AdaptToBookViewModel();
        viewModel.BookId = book.BookId;
        
        await readonlyStore.Index(viewModel, cancellationToken);
    }
}