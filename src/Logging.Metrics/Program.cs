using System;
using System.Threading.Tasks;
using MassTransit;
using Metrics.Logging.Consumers.CommandMetrics;
using Metrics.Logging.Consumers.QueryMetrics;

namespace Metrics.Logging
{
    //todo -> Use SimpleInjector
    //todo -> Use .NET Generic Host
    //todo -> Remove hardcoded ConnectionStrings in consumers
    //todo -> Also pass in TransactionId?
    internal class Program
    {
        internal static async Task Main()
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

            try
            {
                await busControl.StartAsync();

                await Console.Out.WriteLineAsync("Logging.Metrics host started");

                while (true)
                {
                    await Task.Delay(5000);
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
