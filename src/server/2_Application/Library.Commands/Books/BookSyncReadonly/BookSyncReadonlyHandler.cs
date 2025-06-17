using CRA.Domain.Mappers;
using Library.GoogleBooks.Models;
using Library.Models.Contracts;
using Library.Models.ViewModels;
using Library.Notifications.Models;
using MediatR;

namespace Library.Commands.Books.BookSyncReadonly;

public record BookSyncReadonlyRequest(Guid BookId) : IRequest;

public class BookSyncReadonlyHandler(
    IGoogleBooksApiClient api, 
    IBookRepository bookRepository,
    IReadonlyStore readonlyStore,
    INotificationClient notificationClient) : IRequestHandler<BookSyncReadonlyRequest>
{
    public async Task Handle(BookSyncReadonlyRequest request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetById(request.BookId, cancellationToken);
        var result = await api.GetBookById(book.VolumeId, cancellationToken);
        
        var viewModel = result.AdaptToBookViewModel();
        viewModel.BookId = book.BookId;
        
        await readonlyStore.Index(viewModel, cancellationToken);

        var catalogItem = new CatalogueItemViewModel
        {
            BookId = request.BookId,
            Book = viewModel,
            Reservations = []
        };
        await notificationClient.NotifyBookAdded(catalogItem, cancellationToken);
    }
}