var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("Library");
var db = sql.AddDatabase("librarydb");

var api = builder.AddProject<Projects.Library_Api>("library-api")
    .WithReference(db);

builder.AddProject<Projects.Library_MessageProcessor>("library-message-processor")
    .WithReference(db)
    .WithReference(api);

builder.Build().Run();
