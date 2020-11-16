using System;
using System.Threading.Tasks;
using MassTransit;
using Metrics.Logging.QueryMetrics;

namespace EloMatches.Api.Application.Bus.EndpointSenders.QueryMetrics
{
    public class LogQueryMetricsEndpointSender : IEndpointSender<LogQueryMetricsCommand>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private const string QueueName = "metrics-query";

        public LogQueryMetricsEndpointSender(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Send(LogQueryMetricsCommand command)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueName}"));
            
            await endpoint.Send<LogQueryMetrics>(new
            {
                CorrelationId = command.CorrelationId,
                TimeStarted = command.TimeStarted,
                TimeFinished = command.TimeFinished,
                QueryTypeName = command.QueryTypeName,
                ExceptionMessage = command.ExceptionMessage
            });
        }
    }
}

// ReSharper disable InconsistentNaming
namespace Metrics.Logging.QueryMetrics
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