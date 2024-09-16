using Application.Configs.Inbox;
using Application.Configs.Outbox;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configs;

public static class ApplicationConfig
{
    public static IServiceCollection ConfigureApplicationInbox(this IServiceCollection services)
    {
        services.AddMediatorInbox();

        return services;
    }

    public static IServiceCollection ConfigureApplicationOutbox(this IServiceCollection services)
    {
        services.AddMediatorOutbox();

        return services;
    }
}
