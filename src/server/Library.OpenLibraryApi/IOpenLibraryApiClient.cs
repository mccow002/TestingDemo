using Refit;

namespace Library.OpenLibraryApi;

public interface IOpenLibraryApiClient
{
    [Get("search.json")]
    Task<SearchResults> Search(SearchParams searchParams);
}