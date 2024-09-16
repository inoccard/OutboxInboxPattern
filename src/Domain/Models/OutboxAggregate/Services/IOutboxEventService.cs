using Domain.Models.OutboxAggregate.Entities;

namespace Domain.Models.OutboxAggregate.Services;

public interface IOutboxEventService
{
    public Task SaveEvent<T>(T eventData);
    public OutboxEvent[] GetPendingEvents();
    public OutboxEvent[] GetEvents();
}