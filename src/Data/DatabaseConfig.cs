using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DatabaseConfig
{
    public static void AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration, string name)
    {
        services.AddDbContext<DataContext>(
            builder => builder.UseSqlServer(configuration.GetConnectionString(name)));
    }

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        var services = app.ApplicationServices.CreateScope().ServiceProvider;
        var dataContext = services.GetRequiredService<DataContext>();
        dataContext.Database.Migrate();
    }
}