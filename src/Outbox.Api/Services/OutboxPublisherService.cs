using MediatR;
using Newtonsoft.Json;
using Outbox.Api.Domain.Models.OutboxAggregrate.Services;
using Outbox.Api.Domain.Models.PersonAggregate.Entities;
using Outbox.Api.Domain.Models.PersonAggregate.Notifications;
using Outbox.Api.Domain.Repository;

namespace Outbox.Api.Services;

public class OutboxPublisherService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = serviceProvider.CreateAsyncScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                mediator.Publish(new PersonJobNotification());
            }

            await Task.Delay(20000, stoppingToken); // Intervalo entre tentativas de processamento
        }

    }
}