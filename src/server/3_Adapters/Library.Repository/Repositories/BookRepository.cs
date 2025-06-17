using CRA.Domain.Mappers;
using Library.Domain.Domain;
using Library.Models.Contracts;
using Library.Models.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Repositories;

public class BookRepository(LibraryContext context, IMediator mediator) : LibraryRepository(context, mediator), IBookRepository
{
    public async Task<Book?> GetById(Guid bookId, CancellationToken token)
    {
        return await context.Set<Book>().FirstOrDefaultAsync(x => x.BookId == bookId, token);
    }
    
    public async Task<List<Book>> GetAllBooks(CancellationToken token)
    {
        return await context.Set<Book>().ToListAsync(token);
    }
    
    public async Task<Book> GetBookForCheckin(Guid bookId, CancellationToken token)
    {
        return await context.Set<Book>()
            .Include(x => x.Checkouts.Where(c => c.ReturnDate == null))
            .Include(x => x.Reservations.Where(r => r.ReservationStatusId == ReservationStatus.Reserved.ReservationStatusId))
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.BookId == bookId, token) ?? throw new InvalidOperationException();
    }
    
    public async Task<List<CatalogueItemViewModel>> GetCatalogueItemsByIds(List<Guid> bookIds, CancellationToken token)
    {
        return await context.Set<Book>()
            .Where(x => bookIds.Contains(x.BookId))
            .Select(BookMapper.ProjectToCatalogueItemViewModel)
            .ToListAsync(token);
    }
    
    public async Task<CheckoutViewModel?> GetCheckout(Guid bookId, CancellationToken token)
    {
        return await context.Set<Checkout>()
            .Where(x => x.BookId == bookId && x.ReturnDate == null)
            .Select(CheckoutMapper.ProjectToViewModel)
            .FirstOrDefaultAsync(token);
    }
    
    public async Task<ReservationViewModel?> GetReservation(Guid reservationId, CancellationToken token)
    {
        return await context.Set<Reservation>()
            .Where(x => x.ReservationId == reservationId)
            .Select(ReservationMapper.ProjectToViewModel)
            .FirstOrDefaultAsync(token);
    }
}