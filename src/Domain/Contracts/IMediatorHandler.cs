namespace Domain.Contracts;

public interface IMediatorHandlerInbox
{
    Task Publish<T>(T @event);
    Task Send<T>(T @event);
}

public interface IMediatorHandlerOutbox
{
    Task Publish<T>(T @event);
    Task Send<T>(T @event);
}