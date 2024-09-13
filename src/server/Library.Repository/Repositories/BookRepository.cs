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
}