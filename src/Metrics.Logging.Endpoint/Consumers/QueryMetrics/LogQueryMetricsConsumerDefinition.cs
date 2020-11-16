using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace Metrics.Logging.Endpoint.Consumers.QueryMetrics
{
    public class LogCommandMetricsConsumerDefinition : ConsumerDefinition<LogQueryMetricsConsumer>
    {
        public LogCommandMetricsConsumerDefinition()
        {
            EndpointName = "metrics-query";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<LogQueryMetricsConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
        }
    }
}