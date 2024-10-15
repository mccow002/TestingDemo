using Library.GoogleBooks.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Library.GoogleBooks;

public static class GoogleBooksBootstrapper
{
    public static IServiceCollection AddGoogleBooks(this IServiceCollection services, IConfiguration config)
    {
        services.AddRefitClient<IGoogleBooksApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(config["GoogleBooksApi:Uri"]));

        return services;
    }
}