using Domain.Contracts;
using MediatR;

namespace Core.Communication;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
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