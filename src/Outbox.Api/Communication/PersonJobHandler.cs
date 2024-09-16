using Domain.Models.OutboxAggregrate.Services;
using Domain.Models.PersonAggregate.Notifications;
using Domain.Repository;
using MediatR;
using Newtonsoft.Json;

namespace Outbox.Api.Communication;

public class PersonJobHandler(
    IOutboxEventService outboxEventService,
    IRepository repository,
    IMediator mediator
) : INotificationHandler<PersonJobNotification>
{
    public async Task Handle(PersonJobNotification notification, CancellationToken cancellationToken)
    {
        var events = outboxEventService.GetPendingEvents();

        foreach (var pendingEvent in events)
            try
            {
                if (!SetEventTypeName().TryGetValue(pendingEvent.Type, out var eventType)) continue;

                var @event = JsonConvert.DeserializeObject(pendingEvent.Payload, eventType);
                await mediator.Publish(@event);

                // Marcar como publicado após sucesso
                pendingEvent.UpdateDate();
                pendingEvent.UpdateRegisteredStatus();

                await repository.CommitAsync();
            }
            catch (Exception)
            {
                // Manter Published como false para tentar novamente depois
                // Logar o erro ou tratar como necessário
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