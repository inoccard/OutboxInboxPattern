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
builder.Services.AddScoped<IRepository, DataContext>();

var app = builder.Build();

app.ApplyMigrations();

app.MapDefaultEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();