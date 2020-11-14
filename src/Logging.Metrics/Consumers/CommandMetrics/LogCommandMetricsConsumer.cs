using System;
using System.Threading.Tasks;
using MassTransit;

namespace Logging.Metrics.Consumers.CommandMetrics
{
    public class LogCommandMetricsConsumer : IConsumer<LogCommandMetrics>
    {
        public Task Consume(ConsumeContext<LogCommandMetrics> context)
        {
            return Console.Out.WriteLineAsync("Consumed command metrics");
        }
    }
}