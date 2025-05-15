using Library.Notifications.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace Library.Notifications;

public static class NotificationsBootstrapper
{
    public static IHostApplicationBuilder AddNotifications(this IHostApplicationBuilder builder)
    {
        builder.Services.AddRefitClient<INotificationClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["NotificationApi:Uri"]));

        return builder;
    }
}