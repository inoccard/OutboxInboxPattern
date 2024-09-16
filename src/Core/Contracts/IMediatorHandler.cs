namespace Core.Contracts;

public interface IMediatorHandler
{
    Task Publish<T>(T @event);
    Task Send<T>(T @event);
}