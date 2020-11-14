using System;
using System.Threading.Tasks;
using Logging.Metrics.Consumers.QueryMetrics;
using MassTransit;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Logging.Metrics.Tests
{
    //[Ignore("Run-Ad-Hoc")]
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
                    CorrelationId = Guid.NewGuid(),
                    TimeStarted = new DateTime(2020, 11, 14, 10, 00, 00),
                    TimeFinished = new DateTime(2020, 11, 14, 10, 00, 00),
                    QueryTypeName = "PlayerByIdQuery",
                    ExceptionMessage = null as string

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
        Guid CorrelationId { get; }
        DateTime TimeStarted { get; }
        DateTime TimeFinished { get; }
        string QueryTypeName { get; }
        string? ExceptionMessage { get; }
    }
}