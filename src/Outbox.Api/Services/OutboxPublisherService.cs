using Application.Outbox;
using Domain.Contracts;

namespace Outbox.Api.Services;

public class OutboxPublisherService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = serviceProvider.CreateAsyncScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandlerOutbox>();
                await mediator.Publish(new OutboxJobNotification());
            }

            await Task.Delay(20000, stoppingToken); // Intervalo entre tentativas de processamento
        }
    }
}