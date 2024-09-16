using Domain.Models.PersonAggregate.Notifications;
using MassTransit;
using MediatR;
using MessageQueue.Messages;

namespace Outbox.Api.Communication;

public class PersonHandler(IBusControl busControl, ILogger<PersonHandler> logger) :
    INotificationHandler<PersonCreatedNotification>
{
    public async Task Handle(PersonCreatedNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            await busControl.Publish<IPersonCreated>(new
            {
                notification.Id,
                notification.Name,
                notification.Document,
                notification.DocumentType
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw;
        }
    }
}