using Microsoft.Extensions.DependencyInjection;

namespace Library.Api.AppHost;

public static class CliCommandExtensions
{
    public static IResourceBuilder<T> WithSyncReadonlyCommand<T>(this IResourceBuilder<T> builder, string endpointName = "https")
        where T : class, IResourceWithEndpoints
    {
        builder.ApplicationBuilder.Services.AddHttpClient();
        
        return builder
            .WithCommand(
                name: "sync-readonly",
                displayName: "Sync Readonly",
                executeCommand: context => ExecuteSyncCommand(builder, context, endpointName)
            );
    }

    private static async Task<ExecuteCommandResult> ExecuteSyncCommand<T>(IResourceBuilder<T> builder,
        ExecuteCommandContext context, string endpointName) where T : IResourceWithEndpoints
    {
        var url = builder.Resource.GetEndpoint(endpointName).Url;
        var clientFactory = context.ServiceProvider.GetRequiredService<IHttpClientFactory>();
        var client = clientFactory.CreateClient();
        client.BaseAddress = new Uri(url);
        
        using var req = new HttpRequestMessage(HttpMethod.Post, "/catalogue/resync");
        using var rsp = await client.SendAsync(req, HttpCompletionOption.ResponseContentRead);
        
        return rsp.IsSuccessStatusCode ? 
            CommandResults.Success() : 
            CommandResults.Failure(await rsp.Content.ReadAsStringAsync());
    }
    
    // public static IResourceBuilder<T> WithSyncReadonlyCommand<T>(this IResourceBuilder<T> builder)
    //     where T : class, IResource
    // {
    //     return builder
    //         .WithCommand(
    //             name: "sync-readonly",
    //             displayName: "Sync Readonly",
    //             executeCommand: context => ExecuteSyncCommand(builder, context)
    //         );
    // }
    //
    // private static async Task<ExecuteCommandResult> ExecuteSyncCommand<T>(IResourceBuilder<T> builder, ExecuteCommandContext context) where T : IResource
    // {
    //     var cli = builder.ApplicationBuilder.Resources.FirstOrDefault(x => x.Name == "library-cli");
    //     var syncArgs = new CommandLineArgsCallbackAnnotation(ctx => ctx.Add("sync-readonly"));
    //     cli.Annotations.Add(syncArgs);
    //
    //     var notificationSerivce = context.ServiceProvider.GetRequiredService<ResourceNotificationService>();
    //     var evtResource = await notificationSerivce.WaitForResourceAsync(cli.Name, _ => true, CancellationToken.None);
    //     
    //     var startCommand = cli.Annotations.OfType<ResourceCommandAnnotation>().First(x => x.Name == "resource-start");
    //     await startCommand.ExecuteCommand(new ExecuteCommandContext
    //     {
    //         ResourceName = evtResource.ResourceId,
    //         CancellationToken = CancellationToken.None,
    //         ServiceProvider = context.ServiceProvider
    //     });
    //     
    //     cli.Annotations.Remove(syncArgs);
    //     
    //     return CommandResults.Success();
    // }
}