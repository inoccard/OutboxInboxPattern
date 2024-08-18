namespace Outbox.Api.Data.Helpers;

public abstract class Document
{
    public int Id { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
}