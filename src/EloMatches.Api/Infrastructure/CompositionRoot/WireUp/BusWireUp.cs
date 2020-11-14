using EloMatches.Api.Application.Bus.EndpointSenders;
using EloMatches.Api.Application.Bus.EndpointSenders.Behaviors;
using MassTransit;
using SimpleInjector;

namespace EloMatches.Api.Infrastructure.CompositionRoot.WireUp
{
    public static class BusWireUp
    {
        public static Container RegisterBusControl(this Container container) //Func to ServiceHost
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

            return container;
        }
    }
}