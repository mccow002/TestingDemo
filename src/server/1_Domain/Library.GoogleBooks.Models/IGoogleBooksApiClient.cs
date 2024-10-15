using Refit;

namespace Library.GoogleBooks.Models;

public interface IGoogleBooksApiClient
{
    [Get("/books/v1/volumes?q={searchTerm}&key={apiKey}")]
    Task<SearchResults> Search([AliasAs("searchTerm")] string searchTerm, [AliasAs("apiKey")] string apiKey);

    [Get("/books/v1/volumes/{id}?key={apiKey}")]
    Task<SearchItem> GetBookById([AliasAs("id")] string id, [AliasAs("apiKey")] string apiKey);
}