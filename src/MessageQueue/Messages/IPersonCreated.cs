namespace MessageQueue.Messages;

public interface IPersonCreated
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public int DocumentType { get; set; }
}