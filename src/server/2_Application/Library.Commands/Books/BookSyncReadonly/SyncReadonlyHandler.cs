using CRA.Domain.Mappers;
using Library.GoogleBooks.Models;
using Library.Models.Contracts;
using MediatR;

namespace Library.Commands.Books.BookSyncReadonly;

public record SyncReadonlyRequest : IRequest;

public class SyncReadonlyHandler(
    IBookReadonlyRepository repo,
    IGoogleBooksApiClient api,
    IReadonlyStore readonlyStore) : IRequestHandler<SyncReadonlyRequest>
{
    public async Task Handle(SyncReadonlyRequest request, CancellationToken cancellationToken)
    {
        var books = await repo.GetAllBooks(cancellationToken);
        foreach (var book in books)
        {
            var result = await api.GetBookById(book.VolumeId, cancellationToken);
            var viewModel = result.AdaptToBookViewModel();
            viewModel.BookId = book.BookId;
        
            await readonlyStore.Index(viewModel, cancellationToken);
        }
    }
}