using MassTransit;
using MediatR;
using MessageQueue.Messages;
using Outbox.Api.Domain.Models.PersonAggregate.Notifications;

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
                Id = notification.Id,
                Name = notification.Name,
                Document = notification.Document,
                DocumentType = notification.DocumentType
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw;
        }
    }
}