using System.ComponentModel.DataAnnotations;

namespace Domain.Models.OutboxAggregrate.Entities;

public class OutboxEvent
{
    public OutboxEvent(string type, string payload)
    {
        Type = type;
        Payload = payload;
    }

    [Key] public int Id { get; set; }

    public string Type { get; private set; }
    public string Payload { get; private set; }
    public string Error { get; private set; }
    public int Attempts { get; private set; }
    public int Status { get; private set; } = 1;
    public DateTime RegisteredDate { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; private set; } = DateTime.UtcNow;

    public void UpdateAttaments() => Attempts += 1;

    public void UpdateError(string error) => Error = error;

    public void UpdateRegisteredStatus() => Status = 2;

    public void UpdateDate() => UpdatedDate = DateTime.UtcNow;
}