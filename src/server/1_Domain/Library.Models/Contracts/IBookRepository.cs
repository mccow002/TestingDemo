using Library.Domain.Contracts;
using Library.Domain.Domain;
using Library.Models.ViewModels;

namespace Library.Models.Contracts;

public interface IBookRepository : ILibraryRepository
{
    Task<Book?> GetById(Guid bookId, CancellationToken token);
    Task<List<Book>> GetAllBooks(CancellationToken token);
    Task<CheckoutViewModel?> GetCheckout(Guid bookId, CancellationToken token);
    Task<List<CatalogueItemViewModel>> GetCatalogueItemsByIds(List<Guid> bookIds, CancellationToken token);
    Task<ReservationViewModel?> GetReservation(Guid reservationId, CancellationToken token);
    Task<Book> GetBookForCheckin(Guid bookId, CancellationToken token);
}