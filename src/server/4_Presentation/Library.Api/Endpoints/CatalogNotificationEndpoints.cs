using Library.Models.Books;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Library.Api.Endpoints;

public static class CatalogNotificationEndpoints
{
    public static IEndpointRouteBuilder MapCatalogNotificationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var catalogNotifications = endpoints.MapGroup("catalog/notifications");

        catalogNotifications.MapPost("/book-added",
            async ([FromBody] CatalogueItemViewModel model, IHubContext<NotificationHub, INotificationHub> hub) =>
            {
                await hub.Clients.All.BookAdded(model);
            });

        return catalogNotifications;
    }
}