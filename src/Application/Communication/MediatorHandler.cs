using Domain.Contracts;
using MediatR;

namespace Application.Communication;

public class MediatorHandlerInbox(IMediator mediator) : IMediatorHandlerInbox
{
    public async Task Publish<T>(T @event)
    {
        await mediator.Publish(@event);
    }

    public async Task Send<T>(T @event)
    {
        await mediator.Send(@event);
    }
}

public class MediatorHandlerOutbox(IMediator mediator) : IMediatorHandlerOutbox
{
    public async Task Publish<T>(T @event)
    {
        await mediator.Publish(@event);
    }

    public async Task Send<T>(T @event)
    {
        await mediator.Send(@event);
    }
}