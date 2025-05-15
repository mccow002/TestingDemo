using Library.Api;
using Library.Api.Endpoints;
using Library.Api.ServiceDefaults;
using Library.Commands;
using Library.DomainEvents;
using Library.Elasticsearch;
using Library.GoogleBooks;
using Library.Notifications;
using Library.Notifications.Models;
using Library.Queries;
using Library.Repository;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(c => c.AddDefaultPolicy(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.AddRepository();
builder.Services.AddLibraryCommands();
builder.Services.AddLibraryQueries();
builder.Services.AddGoogleBooks(builder.Configuration);
builder.Services.AddElasticsearch(builder.Configuration);
builder.Services.AddDomainEvents();
builder.AddNotifications();

builder.Services.AddMassTransit(mt =>
{
    mt.UsingRabbitMq((ctx, cfg) =>
    {
        var uri = builder.Configuration["RabbitMq:Uri"];
        cfg.Host(uri);
        
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
}

app.UseHttpsRedirection();
app.UseCors();

app
    .MapBookEndpoints()
    .MapCatalogueEndpoints()
    .MapCatalogNotificationEndpoints()
    .MapUserEndpoints();

app.MapHub<NotificationHub>("/notifications");   

app.Run();

public partial class Program;
