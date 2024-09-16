using Application.Outbox;
using MassTransit;
using MediatR;
using MessageQueue.Messages;
using Microsoft.Extensions.Logging;

namespace MessageQueue.Outbox.Publishers;

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
            }, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw;
        }
    }
}