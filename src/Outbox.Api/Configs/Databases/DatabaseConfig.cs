using Outbox.Api.Data;
using Outbox.Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Outbox.Api.Configs.Databases;

public static class DatabaseConfig
{
    public static void AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Outbox");
        services.AddDbContext<DataContext>(
            builder => builder.UseSqlServer(connection));
    }
    
    public static void ApplyMigrations(this WebApplication app)
    {
        var services = app.Services.CreateScope().ServiceProvider;
        var dataContext = services.GetRequiredService<DataContext>();
        dataContext.Database.Migrate();
    }
}