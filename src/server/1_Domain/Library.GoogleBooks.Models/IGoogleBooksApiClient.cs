using Refit;

namespace Library.GoogleBooks.Models;

public interface IGoogleBooksApiClient
{
    [Get("/books/v1/volumes?q={searchTerm}")]
    Task<SearchResults> Search([AliasAs("searchTerm")] string searchTerm, CancellationToken token);

    [Get("/books/v1/volumes/{id}")]
    Task<SearchItem> GetBookById([AliasAs("id")] string id, CancellationToken token);
}