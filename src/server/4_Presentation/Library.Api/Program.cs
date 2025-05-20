using Library.Api.Endpoints;
using Library.Api.ServiceDefaults;
using Library.Commands;
using Library.DomainEvents;
using Library.Elasticsearch;
using Library.GoogleBooks;
using Library.Notifications;
using Library.Queries;
using Library.Repository;
using MassTransit;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddCors(c => c
    .AddDefaultPolicy(p => p
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(builder.Configuration["Origin"])
    )
);

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("Library"), tags: ["live"])
    .AddElasticsearch(builder.Configuration.GetConnectionString("elasticsearch"), tags: ["live"]);

builder.AddRepository();
builder.Services.AddLibraryCommands();
builder.Services.AddLibraryQueries();
builder.Services.AddDomainEvents();
builder.AddGoogleBooks();
builder.AddElasticsearch();
builder.AddNotifications();

builder.Services.AddServiceDiscoveryCore();
builder.Services.AddConfigurationServiceEndpointProvider();

builder.Services.AddMassTransit(mt =>
{
    mt.UsingAzureServiceBus((ctx, cfg) =>
    {
        var connStr = builder.Configuration.GetConnectionString("service-bus");
        Console.WriteLine(connStr);
        cfg.Host(new Uri(connStr.Replace("https", "sb")));
        
        cfg.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();

//app.UseMiddleware<ProviderStateMiddleware>();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors();

app
    .MapBookEndpoints()
    .MapCatalogueEndpoints()
    .MapCatalogNotificationEndpoints()
    .MapUserEndpoints();

app.MapHub<NotificationHub>("/notifications");

app.MapGet("/config", (IConfiguration config) => ((IConfigurationRoot)config).GetDebugView());

app.Run();

public partial class Program;
