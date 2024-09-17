using Domain.Models.InboxAggregate.Entities;

namespace Domain.Models.InboxAggregate.Services;

public interface IInboxEventService
{
    public Task SaveEvent<T>(T eventData);
    public Task SaveEvent(InboxEvent @event);
    public InboxEvent[] GetPendingEvents();
    public InboxEvent[] GetEvents();
}