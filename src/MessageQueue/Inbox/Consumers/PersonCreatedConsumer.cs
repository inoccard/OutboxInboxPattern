using Application.Inbox;
using Domain.Contracts;
using Domain.Models.PersonAggregate.Enums;
using MassTransit;
using MessageQueue.Messages;

namespace MessageQueue.Inbox.Consumers;

public class PersonCreatedConsumer(IMediatorHandlerInbox mediator) : IConsumer<IPersonCreated>
{
    public async Task Consume(ConsumeContext<IPersonCreated> context)
    {
        try
        {
            var message = context.Message;
            await mediator.Publish(new CreatePersonNotification(
                message.Id,
                message.Name,
                message.Document,
                (DocumentType)message.DocumentType));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}