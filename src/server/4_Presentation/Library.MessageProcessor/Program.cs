using Library.Api.ServiceDefaults;
using Library.Commands;
using Library.DomainEvents;
using Library.Elasticsearch;
using Library.MessageProcessor;
using Library.OpenLibraryApi;
using Library.Queries;
using Library.Repository;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.AddRepository();
builder.Services.AddLibraryCommands();
builder.Services.AddLibraryQueries();
builder.Services.AddOpenLibraryApi(builder.Configuration);
builder.Services.AddElasticsearch(builder.Configuration);
builder.Services.AddDomainEvents();

builder.Services.AddMassTransit(mt =>
{
    mt.AddConsumers(typeof(Program).Assembly);
    
    mt.UsingRabbitMq((ctx, cfg) =>
    {
        var uri = builder.Configuration["RabbitMq:Uri"];
        cfg.Host(uri);
        
        cfg.ConfigureEndpoints(ctx);
    });
});

var host = builder.Build();
await host.RunAsync();

public partial class Program;