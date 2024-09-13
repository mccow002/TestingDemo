using Elastic.Clients.Elasticsearch;
using Library.Models.Contracts;

namespace Library.Elasticsearch;

public class ReadonlyStore(ElasticsearchClient client) : IReadonlyStore
{
    public async Task Index<T>(T document, CancellationToken token) where T : class
    {
        await client.IndexAsync(document, token);
    }
}