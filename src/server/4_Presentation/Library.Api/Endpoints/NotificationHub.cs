using Library.Models.Books;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace Library.Api.Endpoints;

public class NotificationHub : Hub<INotificationHub>
{
    
}

public interface INotificationHub
{
    [HubMethodName("book-added")]
    Task BookAdded(CatalogueItemViewModel model);
}