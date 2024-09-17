using Application.Inbox;
using Domain.Contracts;

namespace Inbox.Api.Services;

public class InboxPublisherService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = serviceProvider.CreateAsyncScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandlerInbox>();
                await mediator.Publish(new InboxJobNotification());
            }

            await Task.Delay(20000, stoppingToken);
        }
    }
}