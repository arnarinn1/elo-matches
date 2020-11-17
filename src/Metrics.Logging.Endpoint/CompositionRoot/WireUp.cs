using System.Data;
using MassTransit;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Metrics.Logging.Endpoint.CompositionRoot
{
    public class WireUp
    {
        public static void RegisterServices(HostBuilderContext context, IServiceCollection services)
        {
            var connectionString = context.Configuration["ConnectionString"];

            var massTransitConfigSection = context.Configuration.GetSection("MassTransit").Get<MassTransitConfiguration>();

            services.AddMassTransit(config =>
            {
                config.AddConsumers(typeof(Program).Assembly);

                config.UsingRabbitMq((busContext, rabbitMqConfigurator) =>
                {
                    rabbitMqConfigurator.Host(massTransitConfigSection.Host, massTransitConfigSection.VirtualHost, x =>
                    {
                        x.Username(massTransitConfigSection.Username);
                        x.Password(massTransitConfigSection.Password);
                    });

                    rabbitMqConfigurator.ConfigureEndpoints(busContext);
                });
            });

            services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));

            services.AddHostedService<HostedServiceForBusControl>();
        }
    }
}