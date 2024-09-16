using Newtonsoft.Json;
using Outbox.Api.Domain.Models.OutboxAggregrate.Entities;
using Outbox.Api.Domain.Models.PersonAggregate.Entities;
using Outbox.Api.Domain.Repository;

namespace Outbox.Api.Domain.Models.OutboxAggregrate.Services;

public class OutboxEventService(IRepository repository)
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

    public IEnumerable<object?> GetPendingEvents()
    {
        var events = repository.QueryIncludeStringProperties<OutboxEvent>()
            .Where(o => o.Status == 1 && o.Attempts < 3)
            .ToArray();

        var result = new List<object>();

        foreach (var @event in events)
        {
            if (!SetEventTypeName().TryGetValue(@event.Type, out var eventType)) continue;

            var data = JsonConvert.DeserializeObject(@event.Payload, eventType);
            if (data != null) result.Add(data);
        }

        return result;
    }
    
    public IEnumerable<object?> GetEvents()
    {
        var events = repository.QueryIncludeStringProperties<OutboxEvent>()
            .Where(o => o.Status == 1 && o.Attempts < 3)
            .ToArray();

        return events;
    }

    private static Dictionary<string, Type> SetEventTypeName() =>
        new()
        {
            { "Person", typeof(Person) }
        };
}