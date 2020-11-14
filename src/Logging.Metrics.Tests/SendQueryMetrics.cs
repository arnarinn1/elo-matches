using System;
using System.Threading.Tasks;
using Logging.Metrics.Consumers.QueryMetrics;
using MassTransit;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Logging.Metrics.Tests
{
    [Ignore("Run-Ad-Hoc")]
    [TestFixture, Category("Debug")]
    public class SendQueryMetrics
    {
        [Test]
        public async Task Send()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc => { sbc.Host("rabbitmq://localhost"); });

            try
            {
                await bus.StartAsync();

                var endpoint = await bus.GetSendEndpoint(new Uri("queue:metrics-query"));

                await endpoint.Send<LogQueryMetrics>(new
                {
                    Identifier = Guid.NewGuid()
                });
            }
            finally
            {
                await bus.StopAsync();
            }
        }
    }
}

namespace Logging.Metrics.Consumers.QueryMetrics
{
    public interface LogQueryMetrics
    {
        Guid Identifier { get; }
    }
}