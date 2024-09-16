using Core.Communication;
using Core.Contracts;
using MessageQueue;
using Outbox.Api.Data;
using Outbox.Api.Domain.Models.OutboxAggregrate.Services;
using Outbox.Api.Domain.Repository;

namespace Outbox.Api.Configs;

public static class RegisterAppServices
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediator()
            .AddScoped<IRepository, DataContext>()
            .AddScoped<OutboxEventService>()
            .AddScoped<IMediatorHandler, MediatorHandler>();
    }
}