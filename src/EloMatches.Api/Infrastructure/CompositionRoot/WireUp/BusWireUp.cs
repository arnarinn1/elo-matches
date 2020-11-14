using EloMatches.Api.Application.Bus.EndpointSenders;
using EloMatches.Api.Application.Bus.EndpointSenders.Behaviors;
using EloMatches.Api.Infrastructure.CompositionRoot.Implementations;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace EloMatches.Api.Infrastructure.CompositionRoot.WireUp
{
    public static class BusWireUp
    {
        public static Container RegisterBusControl(this Container container, IServiceCollection serviceCollection)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            container.RegisterSingleton<IBusControl>(() => busControl);
            container.RegisterSingleton<ISendEndpointProvider>(() => busControl);

            container.Register(typeof(IEndpointSender<>), typeof(IEndpointSender<>).Assembly);
            container.RegisterDecorator(typeof(IEndpointSender<>), typeof(RetryBehaviorForEndpointSender<>));

            serviceCollection.AddHostedService(_ => new MassTransitBusHostedService(busControl));

            return container;
        }
    }
}