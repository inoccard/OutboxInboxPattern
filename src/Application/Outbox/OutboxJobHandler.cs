using Domain.Models.OutboxAggregate.Notifications;
using Domain.Models.OutboxAggregate.Services;
using Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Outbox;

public class OutboxJobHandler(
    IOutboxEventService outboxEventService,
    IRepository repository,
    IMediator mediator,
    ILogger<OutboxJobHandler> logger) : INotificationHandler<OutboxJobNotification>
{
    public async Task Handle(OutboxJobNotification notification, CancellationToken cancellationToken)
    {
        var events = outboxEventService.GetPendingEvents();

        foreach (var pendingEvent in events)
            try
            {
                if (!SetEventTypeName().TryGetValue(pendingEvent.Type, out var eventType)) continue;

                var @event = JsonConvert.DeserializeObject(pendingEvent.Payload, eventType);
                await mediator.Publish(@event, cancellationToken);

                // Marcar como publicado após sucesso
                pendingEvent.UpdateDate();
                pendingEvent.UpdateRegisteredStatus();

                await repository.CommitAsync();
            }
            catch (Exception ex)
            {
                var message = $"{ex.Message} {ex.InnerException?.Message}";
                logger.LogError(message);
                @pendingEvent.UpdateError(message);
                pendingEvent.UpdateAttaments();
                await repository.CommitAsync();
            }
    }

    private static Dictionary<string, Type> SetEventTypeName()
    {
        return new Dictionary<string, Type>
        {
            { "PersonCreatedNotification", typeof(PersonCreatedNotification) }
        };
    }
}