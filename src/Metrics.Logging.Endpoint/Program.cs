using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Metrics.Logging.Endpoint.Consumers.CommandMetrics;
using Metrics.Logging.Endpoint.Consumers.QueryMetrics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Metrics.Logging.Endpoint
{
    //todo -> Use SimpleInjector
    //todo -> Use .NET Generic Host
    //todo -> Remove hardcoded ConnectionStrings in consumers
    //todo -> Also pass in TransactionId?
    internal class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host("localhost", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        cfg.ReceiveEndpoint("metrics-query", e =>
                        {
                            e.Consumer<LogQueryMetricsConsumer>();
                        });

                        cfg.ReceiveEndpoint("metrics-command", e =>
                        {
                            e.Consumer<LogCommandMetricsConsumer>();
                        });
                    });

                    services.AddSingleton<IBusControl>(_ => busControl);

                    services.AddHostedService<HostedServiceForBusControl>();
                });

        public class HostedServiceForBusControl : IHostedService
        {
            private readonly IBusControl _busControl;

            public HostedServiceForBusControl(IBusControl busControl)
            {
                _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
            }

            public async Task StartAsync(CancellationToken cancellationToken)
            {
                await _busControl.StartAsync(cancellationToken);
            }

            public async Task StopAsync(CancellationToken cancellationToken)
            {
                await _busControl.StopAsync(cancellationToken);
            }
        }
    }
}
