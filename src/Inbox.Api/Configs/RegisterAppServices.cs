using Data;
using Domain.Models.InboxAggregate.Services;
using Domain.Models.PersonAggregate.Services;
using Domain.Repository;

namespace Inbox.Api.Configs;

public static class RegisterAppServices
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
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

        services.AddScoped<IRepository, DataContext>();
        services.AddScoped<IInboxEventService, InboxEventService>();
        services.AddScoped<IPersonService, PersonService>();

        return services;
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