namespace MessageQueue;

public class QueueSettings
{
    public string Address { get; set; }
    public int Port { get; set; }
    public string VirtualHost { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int RetryCount { get; init; }
    public int RetryInterval { get; init; }
}
