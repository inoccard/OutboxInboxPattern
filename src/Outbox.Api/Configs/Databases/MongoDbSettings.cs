namespace Outbox.Api.Configs.Databases;

public class MongoDbSettings
{
    public string UserName { get; init; }
    public string Password { get; init; }
    public string ClusterEndpoint { get; init; }
    public string DatabaseName { get; init; }
    public int ExpireDays { get; init; }

    public string GetConnectionString() => $"mongodb://{ClusterEndpoint}/{DatabaseName}";
}