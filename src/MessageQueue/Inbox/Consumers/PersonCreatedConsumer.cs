using MassTransit;
using MessageQueue.Messages;

namespace MessageQueue.Inbox.Consumers;

public class PersonCreatedConsumer : IConsumer<IPersonCreated>
{
    public async Task Consume(ConsumeContext<IPersonCreated> context)
    {
        await Task.FromResult(context.Message);
    }
}