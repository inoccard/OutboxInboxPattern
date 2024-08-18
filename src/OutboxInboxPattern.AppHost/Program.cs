var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Outbox_Api>("outbox-api");

builder.AddProject<Projects.Inbox_Api>("inbox-api");

await builder.Build().RunAsync();
