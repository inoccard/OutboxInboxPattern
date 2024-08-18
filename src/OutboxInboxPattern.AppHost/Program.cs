var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Outbox_Api>("outboxapi");

await builder.Build().RunAsync();
