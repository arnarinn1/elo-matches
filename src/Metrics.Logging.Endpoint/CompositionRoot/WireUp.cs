using System.Data;
using MassTransit;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Metrics.Logging.Endpoint.CompositionRoot
{
    public class WireUp
    {
        public static void RegisterServices(HostBuilderContext context, IServiceCollection services)
        {
            var connectionString = context.Configuration["ConnectionString"];

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

            services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));

            services.AddHostedService<HostedServiceForBusControl>();
        }
    }
}