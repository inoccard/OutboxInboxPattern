using MessageQueue;
using Outbox.Api.Configs;
using Outbox.Api.Configs.Databases;
using Outbox.Api.Data;
using Outbox.Api.Domain.Repository;

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

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();