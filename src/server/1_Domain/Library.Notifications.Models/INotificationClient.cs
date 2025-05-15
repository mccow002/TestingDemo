using Library.Models.Books;
using Library.Models.ViewModels;
using Refit;

namespace Library.Notifications.Models;

public interface INotificationClient
{
    [Post("/catalog/notifications/book-added")]
    Task NotifyBookAdded([Body] CatalogueItemViewModel model, CancellationToken cancellationToken = default);
}