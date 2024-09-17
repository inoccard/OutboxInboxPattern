using Application.Inbox;
using Domain.Contracts;
using MassTransit;
using MessageQueue.Messages;
using Newtonsoft.Json;

namespace MessageQueue.Consumers.Inbox;

public class PersonCreatedConsumerError(IMediatorHandlerInbox mediator) : IConsumer<Fault<IPersonCreated>>
{
    public async Task Consume(ConsumeContext<Fault<IPersonCreated>> context)
    {
        try
        {
            var message = context.Message.Message;
            await mediator.Publish(new AddInboxNotification(
                nameof(CreatePersonNotification),
                JsonConvert.SerializeObject(message),
                null));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}