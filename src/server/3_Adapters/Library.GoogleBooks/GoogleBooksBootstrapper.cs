using Library.GoogleBooks.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace Library.GoogleBooks;

public static class GoogleBooksBootstrapper
{
    public static IHostApplicationBuilder AddGoogleBooks(this IHostApplicationBuilder builder)
    {
        builder.Services.AddRefitClient<IGoogleBooksApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("http+https://books-api"));

        return builder;
    }
}