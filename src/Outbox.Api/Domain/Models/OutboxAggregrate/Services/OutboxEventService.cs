using Microsoft.EntityFrameworkCore;
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

    public async Task<OutboxEvent[]> GetPendingEvents()
    {
        var events = await repository.Query<OutboxEvent>()
            .Where(o => o.Status == 1 && o.Attempts < 3)
            .ToArrayAsync();

        return events;
    }
    
    public IEnumerable<object?> GetEvents()
    {
        var events = repository.QueryIncludeStringProperties<OutboxEvent>()
            .ToArray();

        return events;
    }
}