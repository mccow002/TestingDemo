using Elastic.Clients.Elasticsearch;
using Library.Models.Books;
using Library.Models.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Elasticsearch;

public static class ElasticsearchBootstrapper
{
    public static IServiceCollection AddElasticsearch(this IServiceCollection services)
    {
        services.AddSingleton<ElasticsearchClient>(_ =>
        {
            var settings = new ElasticsearchClientSettings()
                .DefaultMappingFor<BookViewModel>(x => x.IndexName(nameof(BookViewModel).ToLower()));
            return new ElasticsearchClient(settings);
        });

        services.AddTransient<IReadonlyStore, ReadonlyStore>();
        services.AddTransient<IBookReadonlyRepository, BookReadonlyRepository>();
        
        return services;
    }
}