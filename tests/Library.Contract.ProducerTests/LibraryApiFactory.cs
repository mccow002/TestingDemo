using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.ContractTests;

public class LibraryApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureServices(services =>
        {
            services.AddTransient<IStartupFilter, ProviderStateMiddlewareStartupFilter>();
        });

        builder.UseSetting("urls", "http://localhost:5000");
        builder.UseUrls("http://localhost:5000");

        // builder.Configure(app =>
        // {
        //     app.Map("/provider-states", app =>
        //     {
        //         app.Run(async context =>
        //         {
        //             context.Response.StatusCode = 200;
        //         });
        //     });
        // });
        
        base.ConfigureWebHost(builder);
    }
}