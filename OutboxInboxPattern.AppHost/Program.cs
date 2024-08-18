var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.OutboxInboxPattern>("outboxinboxpattern");

await builder.Build().RunAsync();
