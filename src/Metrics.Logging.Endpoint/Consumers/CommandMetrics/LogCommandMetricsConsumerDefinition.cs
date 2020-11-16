using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace Metrics.Logging.Endpoint.Consumers.CommandMetrics
{
    public class LogCommandMetricsConsumerDefinition : ConsumerDefinition<LogCommandMetricsConsumer>
    {
        public LogCommandMetricsConsumerDefinition()
        {
            EndpointName = "metrics-command";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<LogCommandMetricsConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
        }
    }
}