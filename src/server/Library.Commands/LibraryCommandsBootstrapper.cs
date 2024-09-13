using Microsoft.Extensions.DependencyInjection;

namespace Library.Commands;

public static class LibraryCommandsBootstrapper
{
    public static IServiceCollection AddLibraryCommands(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<ILibraryCommandsMarker>();
        });
        return services;
    }
}