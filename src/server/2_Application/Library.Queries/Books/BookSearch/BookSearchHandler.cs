using CRA.Domain.Mappers;
using Library.GoogleBooks.Models;
using Library.Models.Books;
using MediatR;

namespace Library.Queries.Books.BookSearch;

public record BookSearchRequest(string SearchTerm) : IRequest<BookSearchResponse>;

public record BookSearchResponse(int Total, List<BookViewModel> Results);

public class BookSearchHandler(IGoogleBooksApiClient apiClient) : IRequestHandler<BookSearchRequest, BookSearchResponse>
{
    public async Task<BookSearchResponse> Handle(BookSearchRequest request, CancellationToken cancellationToken)
    {
        var results = await apiClient.Search(request.SearchTerm, "AIzaSyCC478lljstyd4uqJQo-Kudqeddx5Osx2o");
        var viewModels = results.Items
            .Select(x => x.AdaptToBookViewModel())
            .ToList();
        
        return new BookSearchResponse(results.TotalItems, viewModels);
    }
}