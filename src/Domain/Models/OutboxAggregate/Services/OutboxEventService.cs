using Domain.Models.OutboxAggregate.Entities;
using Domain.Repository;
using Newtonsoft.Json;

namespace Domain.Models.OutboxAggregate.Services;

public class OutboxEventService(IRepository repository) : IOutboxEventService
{
    public async Task SaveEvent<T>(T eventData)
    {
        var outboxEvent = new OutboxEvent
        (
            typeof(T).Name,
            JsonConvert.SerializeObject(eventData)
        );

        await repository.AddAsync(outboxEvent);
        await repository.CommitAsync();
    }

    public OutboxEvent[] GetPendingEvents()
    {
        var events = repository.Query<OutboxEvent>()
            .Where(o => o.Status == 1 && o.Attempts < 3)
            .ToArray();

        return events;
    }

    public OutboxEvent[] GetEvents()
    {
        var events = repository.QueryIncludeStringProperties<OutboxEvent>()
            .ToArray();

        return events;
    }
}