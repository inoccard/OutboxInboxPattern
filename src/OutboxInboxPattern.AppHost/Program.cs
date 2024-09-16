using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Outbox_Api>("outbox-api");

builder.AddProject<Inbox_Api>("inbox-api");

await builder.Build().RunAsync();