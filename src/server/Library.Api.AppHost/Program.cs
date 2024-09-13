var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Library_Api>("library-api");
builder.AddProject<Projects.Library_MessageProcessor>("library-message-processor");

builder.Build().Run();
