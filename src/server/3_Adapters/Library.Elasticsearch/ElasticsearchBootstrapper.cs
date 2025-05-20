using Elastic.Clients.Elasticsearch;
using Library.Models.Books;
using Library.Models.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Elasticsearch;

public static class ElasticsearchBootstrapper
{
    public static IHostApplicationBuilder AddElasticsearch(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ElasticsearchClient>(_ =>
        {
            var uri = new Uri(builder.Configuration.GetConnectionString("elasticsearch"));
            var settings = new ElasticsearchClientSettings(uri)
                .DefaultMappingFor<BookViewModel>(x => x.IndexName(nameof(BookViewModel).ToLower()));
            return new ElasticsearchClient(settings);
        });

        builder.Services.AddTransient<IReadonlyStore, ReadonlyStore>();
        builder.Services.AddTransient<IBookReadonlyRepository, BookReadonlyRepository>();
        
        return builder;
    }
}