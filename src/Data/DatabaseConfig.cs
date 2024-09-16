using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Configs.Databases;

public static class DatabaseConfig
{
    public static void AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration, string dbaName)
    {
        services.AddDbContext<DataContext>(
            builder => builder.UseInMemoryDatabase(dbaName));
    }
}