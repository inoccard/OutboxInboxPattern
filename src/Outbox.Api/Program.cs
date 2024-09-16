using MessageQueue;
using Outbox.Api.Configs;
using Outbox.Api.Configs.Databases;
using Outbox.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.ConfigureMessageQueue(builder.Configuration);
builder.Services.AddHostedService<OutboxPublisherService>();

var app = builder.Build();

app.MapControllers();
app.UseApp();

await app.RunAsync();