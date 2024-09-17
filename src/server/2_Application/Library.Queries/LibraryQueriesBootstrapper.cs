using Microsoft.Extensions.DependencyInjection;

namespace Library.Queries;

public static class LibraryQueriesBootstrapper
{
    public static IServiceCollection AddLibraryQueries(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<ILibraryQueriesMarker>();
        });
        
        return services;
    }
}