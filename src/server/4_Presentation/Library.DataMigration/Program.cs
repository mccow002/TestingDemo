using Library.Api.ServiceDefaults;
using Library.DataMigration;
using Library.Repository;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();
builder.AddDbContext();

var host = builder.Build();
host.Run();
