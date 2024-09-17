using CRA.Domain.Mappers;
using Library.Models.Books;
using Library.OpenLibraryApi.Models;
using MediatR;

namespace Library.Queries.Books.BookSearch;

public record BookSearchRequest(string SearchTerm) : IRequest<BookSearchResponse>;

public record BookSearchResponse(int Total, List<BookViewModel> Results);

public class BookSearchHandler(IOpenLibraryApiClient apiClient) : IRequestHandler<BookSearchRequest, BookSearchResponse>
{
    public async Task<BookSearchResponse> Handle(BookSearchRequest request, CancellationToken cancellationToken)
    {
        var searchParams = new SearchParams { Query = request.SearchTerm };
        var results = await apiClient.Search(searchParams);
        var viewModels = results.Docs
            .Where(x => x.Isbn.Count != 0)
            .Select(x => x.AdaptToBookViewModel())
            .ToList();
        
        return new BookSearchResponse(results.NumFound, viewModels);
    }
}