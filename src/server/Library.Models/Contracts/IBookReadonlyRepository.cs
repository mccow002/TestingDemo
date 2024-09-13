using Library.Models.Books;

namespace Library.Models.Contracts;

public interface IBookReadonlyRepository
{
    Task<List<BookViewModel>> GetAllBooks(CancellationToken token);
}