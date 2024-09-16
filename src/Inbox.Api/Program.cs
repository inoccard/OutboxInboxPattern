using Application.Configs;
using Application.Inbox;
using Data.Configs.Databases;
using Inbox.Api.Configs;
using Inbox.Api.Services;
using MediatR;
using MessageQueue;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseConfig(builder.Configuration, "Inbox");
builder.Services.AddServices(builder.Configuration)
    .ConfigureApplicationInbox()
    .ConfigureMessageQueueInbox(builder.Configuration);

builder.Services.AddTransient<INotificationHandler<CreatePersonNotification>, AddPersonHandler>();

builder.Services.AddHostedService<InboxPublisherService>();

var app = builder.Build();

app.MapControllers();
app.UseApp();

await app.RunAsync();