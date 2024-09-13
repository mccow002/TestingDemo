using Library.OpenLibraryApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Library.OpenLibraryApi;

public static class OpenLibraryApiBootstrapper
{
    public static IServiceCollection AddOpenLibraryApi(this IServiceCollection services)
    {
        services.AddRefitClient<IOpenLibraryApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://openlibrary.org"));

        return services;
    }
}