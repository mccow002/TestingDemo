using Microsoft.Extensions.DependencyInjection;

namespace Library.DomainEvents;

public static class DomainEventsBootstrapper
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IDomainEventsMarker>());
        return services;
    }
}