using MediatR;
using Outbox.Api.Domain.Models.PersonAggregate.Notifications;

namespace Outbox.Api.Communication;

public class PersonHandler : INotificationHandler<PersonCreatedNotification>
{
    public async Task Handle(PersonCreatedNotification notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}