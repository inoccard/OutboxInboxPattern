﻿using System.Diagnostics.CodeAnalysis;
using MassTransit;
using MessageQueue.Inbox.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MessageQueue;

[ExcludeFromCodeCoverage]
public static class Startup
{
    public static void ConfigureMessageQueue(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<QueueSettings>(configuration.GetSection(nameof(QueueSettings)));

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            
            QueueSettings queueSettings = null;
            
            x.UsingRabbitMq((context, cfg) =>
            {
                queueSettings = context.GetRequiredService<IOptions<QueueSettings>>().Value;

                var host = new Uri($"{queueSettings.Address}:{queueSettings.Port}/{queueSettings.VirtualHost}");

                cfg.Host(host, h =>
                {
                    h.Username(queueSettings.Username);
                    h.Password(queueSettings.Password);
                });

                cfg.ConfigureEndpoints(context);

            });
            
            x.AddConsumer<PersonCreatedConsumer>(p =>
                p.UseMessageRetry(u => u.Interval(queueSettings.RetryCount, queueSettings.RetryInterval)));
        });
    }
}