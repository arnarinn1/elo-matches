using EloMatches.Api.Application.Bus.EndpointSenders;
using EloMatches.Api.Application.Bus.EndpointSenders.Behaviors;
using EloMatches.Api.ConfigurationModels;
using EloMatches.Api.Infrastructure.CompositionRoot.Implementations;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace EloMatches.Api.Infrastructure.CompositionRoot.WireUp
{
    public static class BusWireUp
    {
        public static Container RegisterBusControl(this Container container, IConfiguration configuration, IServiceCollection serviceCollection)
        {
            var massTransitConfiguration = configuration.GetSection("MassTransit").Get<MassTransitConfiguration>();

            IBusControl busControl;

            if (massTransitConfiguration.UseRabbitMqTransport)
            {
                busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(massTransitConfiguration.Host, massTransitConfiguration.VirtualHost, h =>
                    {
                        h.Username(massTransitConfiguration.Username);
                        h.Password(massTransitConfiguration.Password);
                    });
                });

                serviceCollection?.AddHostedService(_ => new MassTransitBusHostedService(busControl));
            }
            else
            {
                busControl = Bus.Factory.CreateUsingInMemory(cfg => {});
            }

            container.RegisterSingleton<IBusControl>(() => busControl);
            container.RegisterSingleton<ISendEndpointProvider>(() => busControl);

            container.Register(typeof(IEndpointSender<>), typeof(IEndpointSender<>).Assembly);
            container.RegisterDecorator(typeof(IEndpointSender<>), typeof(RetryBehaviorForEndpointSender<>));

            return container;
        }
    }
}