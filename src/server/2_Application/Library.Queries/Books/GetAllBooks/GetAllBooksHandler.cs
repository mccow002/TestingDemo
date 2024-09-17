using Library.Models.Contracts;
using Library.Models.ViewModels;
using MediatR;

namespace Library.Queries.Books.GetAllBooks;

public record GetAllBooksRequest : IRequest<List<CatalogueItemViewModel>>;

public class GetAllBooksHandler(
    IBookReadonlyRepository bookReadonlyRepository,
    IBookRepository bookRepository) : IRequestHandler<GetAllBooksRequest, List<CatalogueItemViewModel>>
{
    public async Task<List<CatalogueItemViewModel>> Handle(GetAllBooksRequest request, CancellationToken cancellationToken)
    {
        var books = await bookReadonlyRepository.GetAllBooks(cancellationToken);
        var bookData = await bookRepository.GetCatalogueItemsByIds(books.Select(x => x.BookId ?? Guid.Empty).ToList(), cancellationToken);

        var catalogue = books.Zip(bookData, (model, book) =>
        {
            book.Book = model;
            return book;
        });
        
        return catalogue.ToList();
    }
}