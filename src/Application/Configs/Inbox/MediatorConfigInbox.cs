using Application.Communication;
using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configs.Inbox;

public static class MediatorConfigInbox
{
    public static IServiceCollection AddMediatorInbox(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(MediatorHandlerInbox)));

        services.AddScoped<IMediatorHandlerInbox, MediatorHandlerInbox>();

        return services;
    }
}
