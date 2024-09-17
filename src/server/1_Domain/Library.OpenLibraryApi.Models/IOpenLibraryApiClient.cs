using Refit;

namespace Library.OpenLibraryApi.Models;

public interface IOpenLibraryApiClient
{
    [Get("/search.json")]
    Task<SearchResults> Search(SearchParams searchParams);

    [Get("/search.json?q={isbn}")]
    Task<SearchResults> GetBookByIsbn([AliasAs("isbn")] string isbn);
}