using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Metrics.Logging.Endpoint.CompositionRoot
{
    public class WireUp
    {
        public static void RegisterServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumers(typeof(Program).Assembly);

                config.UsingRabbitMq((busContext, rabbitMqConfig) =>
                {
                    rabbitMqConfig.Host("localhost", "/", x =>
                    {
                        x.Username("guest");
                        x.Password("guest");
                    });

                    rabbitMqConfig.ConfigureEndpoints(busContext);
                });
            });

            services.AddHostedService<HostedServiceForBusControl>();
        }
    }
}