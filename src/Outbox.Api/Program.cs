using Application.Configs;
using Data;
using MessageQueue;
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

//builder.Services.AddTransient<INotificationHandler<PersonCreatedNotification>, PersonHandler>();
//builder.Services.AddTransient<INotificationHandler<OutboxJobNotification>, OutboxJobHandler>();

builder.Services.AddHostedService<OutboxPublisherService>();

var app = builder.Build();

app.MapControllers();
app.UseApp()
    .ApplyMigrations();

await app.RunAsync();
