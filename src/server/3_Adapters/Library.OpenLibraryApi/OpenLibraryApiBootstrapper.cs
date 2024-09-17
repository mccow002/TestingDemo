using Library.OpenLibraryApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Library.OpenLibraryApi;

public static class OpenLibraryApiBootstrapper
{
    public static IServiceCollection AddOpenLibraryApi(this IServiceCollection services, IConfiguration config)
    {
        services.AddRefitClient<IOpenLibraryApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(config["OpenLibraryApi:Uri"]));

        return services;
    }
}