var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Library_Api>("library-api");

builder.Build().Run();
