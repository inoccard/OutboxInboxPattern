using Core.Communication;
using Core.Contracts;

namespace Outbox.Api.Configs;

public static class MediatorConfig
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(MediatorConfig).Assembly);
        });

        services.AddScoped<IMediatorHandler, MediatorHandler>();

        return services;
    }
}