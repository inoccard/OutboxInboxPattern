using Domain.Models.InboxAggregate.Entities;
using Domain.Models.InboxAggregate.Services;
using MediatR;

namespace Application.Inbox;

public sealed class InboxNotificationHandler(IInboxEventService inboxEventService)
    : INotificationHandler<AddInboxNotification>
{
    public async Task Handle(AddInboxNotification notification, CancellationToken cancellationToken)
    {
        await inboxEventService.SaveEvent(new InboxEvent(
            notification.Type, notification.Payload
        ));
    }
}