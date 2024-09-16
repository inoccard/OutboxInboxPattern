using Application.Configs;
using Application.Outbox;
using Data.Configs.Databases;
using Domain.Models.OutboxAggregate.Notifications;
using MediatR;
using MessageQueue;
using MessageQueue.Outbox.Publishers;
using Outbox.Api.Configs;
using Outbox.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseConfig(builder.Configuration, "Outbox");
builder.Services.AddServices(builder.Configuration)
    .ConfigureApplicationOutbox()
    .ConfigureMessageQueueOutbox(builder.Configuration);

builder.Services.AddTransient<INotificationHandler<PersonCreatedNotification>, PersonHandler>();
builder.Services.AddTransient<INotificationHandler<OutboxJobNotification>, OutboxJobHandler>();

builder.Services.AddHostedService<OutboxPublisherService>();

var app = builder.Build();

app.MapControllers();
app.UseApp();

await app.RunAsync();

