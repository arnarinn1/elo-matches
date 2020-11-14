using EloMatches.Api.Application.Bus.EndpointSenders;
using MassTransit;
using SimpleInjector;

namespace EloMatches.Api.Infrastructure.CompositionRoot.WireUp
{
    public static class BusWireUp
    {
        public static Container RegisterBusControl(this Container container)
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

            return container;
        }
    }
}