using Core.Communication;
using Domain.Contracts;
using Domain.Models.OutboxAggregrate.Services;
using Domain.Repository;
using Outbox.Api.Data;

namespace Outbox.Api.Configs;

public static class RegisterAppServices
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        services.AddMediator()
            .AddScoped<IRepository, DataContext>()
            .AddScoped<IOutboxEventService, OutboxEventService>();
    }

    public static IApplicationBuilder UseApp(this IApplicationBuilder app)
    {
        // Habilitando CORS
        app.UseCors("AllowAll");

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        return app;
    }
}