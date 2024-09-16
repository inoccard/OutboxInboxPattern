using Application.Communication;
using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configs.Outbox;

public static class MediatorConfigOutbox
{
    public static IServiceCollection AddMediatorOutbox(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(MediatorHandlerOutbox)));

        services.AddScoped<IMediatorHandlerOutbox, MediatorHandlerOutbox>();

        return services;
    }
}