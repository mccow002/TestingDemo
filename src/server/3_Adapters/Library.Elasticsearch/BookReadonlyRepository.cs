using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Library.Models.Books;
using Library.Models.Contracts;

namespace Library.Elasticsearch;

public class BookReadonlyRepository(ElasticsearchClient client) : IBookReadonlyRepository
{
    public async Task<List<BookViewModel>> GetAllBooks(CancellationToken token)
    {
        var searchResponse = await client
            .SearchAsync<BookViewModel>(s => s.Query(q => q.MatchAll(new MatchAllQuery())), token);
        return searchResponse.Documents.ToList();
    }
}