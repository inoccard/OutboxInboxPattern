using Domain.Contracts;
using MassTransit;
using MassTransit.Mediator;
using MessageQueue.Messages;

namespace MessageQueue.Inbox.Consumers;

public class PersonCreatedConsumer(IMediator mediator) : IConsumer<IPersonCreated>
{
    public async Task Consume(ConsumeContext<IPersonCreated> context)
    {
        await Task.FromResult(context.Message);
    }
}