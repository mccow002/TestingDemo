// See https://aka.ms/new-console-template for more information

using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using Library.Api.ServiceDefaults;
using Library.CLI;
using Library.CLI.Commands;
using Library.Elasticsearch;
using Library.GoogleBooks;
using Library.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var runner = new CommandLineBuilder(new Root())
    .UseHost(
        _ => Host.CreateDefaultBuilder(),
        host => host
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddEnvironmentVariables();
                builder.AddCommandLine(args);
            })
            .ConfigureHostConfiguration(builder =>
            {
                builder.AddEnvironmentVariables();
                builder.AddCommandLine(args);
            })
            .ConfigureServices((ctx, services) =>
            {
                var host = new CommandLineHostApplicationBuilder(ctx, services);
                host
                    .AddRepository()
                    .AddGoogleBooks()
                    .AddElasticsearch();
                
                services.AddServiceDiscovery();
                
                services.ConfigureHttpClientDefaults(http =>
                {
                    // Turn on resilience by default
                    http.AddStandardResilienceHandler();

                    // Turn on service discovery by default
                    http.AddServiceDiscovery();
                });
            })
            .UseCommandHandler<SyncReadonly, SyncReadonly.Handler>()
    )
    .UseDefaults()
    .Build();

await runner.InvokeAsync(args);