using Domain.Models.OutboxAggregrate.Entities;

namespace Domain.Models.OutboxAggregrate.Services;

public interface IOutboxEventService
{
    public Task SaveEvent<T>(T eventData);
    public OutboxEvent[] GetPendingEvents();
    public OutboxEvent[] GetEvents();
}