using Domain.Models.InboxAggregate.Entities;
using Domain.Repository;
using Newtonsoft.Json;

namespace Domain.Models.InboxAggregate.Services;

public class InboxEventService(IRepository repository) : IInboxEventService
{
    public async Task SaveEvent<T>(T eventData)
    {
        var outboxEvent = new InboxEvent
        (
            typeof(T).Name,
            JsonConvert.SerializeObject(eventData)
        );

        await repository.AddAsync(outboxEvent);
        await repository.CommitAsync();
    }

    public async Task SaveEvent(InboxEvent @event)
    {
        await repository.AddAsync(@event);
        await repository.CommitAsync();
    }

    public InboxEvent[] GetPendingEvents()
    {
        var events = repository.Query<InboxEvent>()
            .Where(o => o.Status == 1 && o.Attempts < 3)
            .ToArray();

        return events;
    }

    public InboxEvent[] GetEvents()
    {
        var events = repository.QueryIncludeStringProperties<InboxEvent>()
            .ToArray();

        return events;
    }
}