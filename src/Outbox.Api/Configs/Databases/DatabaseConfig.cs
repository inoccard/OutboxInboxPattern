﻿using Microsoft.EntityFrameworkCore;
using Outbox.Api.Data;

namespace Outbox.Api.Configs.Databases;

public static class DatabaseConfig
{
    public static void AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Outbox");
        services.AddDbContext<DataContext>(
            builder => builder.UseInMemoryDatabase(connection));
    }
}