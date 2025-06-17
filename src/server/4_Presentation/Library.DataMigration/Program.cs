using System.Diagnostics;
using Library.Api.ServiceDefaults;
using Library.DataMigration;
using Library.Repository;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();
builder.AddDbContext();

builder.Services.AddSingleton<ActivitySource>(_ => new ActivitySource(builder.Environment.ApplicationName));

var host = builder.Build();
host.Run();
