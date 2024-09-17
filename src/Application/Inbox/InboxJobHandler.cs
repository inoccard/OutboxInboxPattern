using Application.Outbox;
using Domain.Contracts;
using Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Inbox;

public class InboxJobHandler(
    //IInboxEventService inboxEventService,
    IRepository repository,
    IMediatorHandlerInbox mediator,
    ILogger<InboxJobHandler> logger) : INotificationHandler<InboxJobNotification>
{
    public async Task Handle(InboxJobNotification notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        //var events = inboxEventService.GetPendingEvents();

        //foreach (var pendingEvent in events)
        //    try
        //    {
        //        if (!SetEventTypeName().TryGetValue(pendingEvent.Type, out var eventType)) continue;

        //        var @event = JsonConvert.DeserializeObject(pendingEvent.Payload, eventType);
        //        await mediator.Publish(@event);

        //        // Marcar como publicado após sucesso
        //        pendingEvent.UpdateDate();
        //        pendingEvent.UpdateRegisteredStatus();

        //        await repository.CommitAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = $"{ex.Message} {ex.InnerException?.Message}";
        //        logger.LogError(message);
        //        @pendingEvent.UpdateError(message);
        //        pendingEvent.UpdateAttaments();
        //        await repository.CommitAsync();
        //    }
    }

    private static Dictionary<string, Type> SetEventTypeName()
    {
        return new Dictionary<string, Type>
        {
            { "PersonCreatedNotification", typeof(PersonCreatedNotification) }
        };
    }
}