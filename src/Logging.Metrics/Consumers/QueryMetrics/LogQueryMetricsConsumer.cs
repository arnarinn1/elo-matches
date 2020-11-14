using System;
using System.Threading.Tasks;
using MassTransit;

namespace Logging.Metrics.Consumers.QueryMetrics
{
    public class LogQueryMetricsConsumer : IConsumer<LogQueryMetrics>
    {
        public Task Consume(ConsumeContext<LogQueryMetrics> context)
        {
            return Console.Out.WriteLineAsync("Consumed query metrics");
        }
    }
}