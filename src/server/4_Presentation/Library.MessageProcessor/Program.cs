using Library.Api.ServiceDefaults;
using Library.Commands;
using Library.DomainEvents;
using Library.Elasticsearch;
using Library.GoogleBooks;
using Library.MessageProcessor;
using Library.Notifications;
using Library.Queries;
using Library.Repository;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("Library"))
    .AddElasticsearch(builder.Configuration.GetConnectionString("elasticsearch"));

builder.AddRepository();
builder.Services.AddLibraryCommands();
builder.Services.AddLibraryQueries();
builder.Services.AddDomainEvents();
builder.AddGoogleBooks();
builder.AddElasticsearch();
builder.AddNotifications();

builder.Services.AddMassTransit(mt =>
{
    mt.AddConsumers(typeof(Program).Assembly);
    
    mt.UsingAzureServiceBus((ctx, cfg) =>
    {
        var connStr = builder.Configuration.GetConnectionString("service-bus");
        cfg.Host(new Uri(connStr.Replace("https", "sb")));
        
        cfg.ConfigureEndpoints(ctx);
    });
});

var host = builder.Build();
await host.RunAsync();

public partial class Program;