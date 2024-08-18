using Outbox.Api.Data;
using Outbox.Api.Domain.Repository;

namespace Outbox.Api.Configs.Databases;

public static class MongoDbConfig
{
    public static void AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
        services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoDbContext<>));
    }
}