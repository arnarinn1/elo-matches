using System;
using System.Threading.Tasks;
using MassTransit;
using Metrics.Logging.Consumers.CommandMetrics;

namespace EloMatches.Api.Application.Bus.EndpointSenders.CommandMetrics
{
    public class LogCommandMetricsEndpointSender : IEndpointSender<LogCommandMetricsCommand>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private const string QueueName = "metrics-command";

        public LogCommandMetricsEndpointSender(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Send(LogCommandMetricsCommand command)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueName}"));

            await endpoint.Send<LogCommandMetrics>(new
            {
                CorrelationId = command.CorrelationId,
                TimeStarted = command.TimeStarted,
                TimeFinished = command.TimeFinished,
                CommandTypeName = command.CommandTypeName,
                ExceptionMessage = command.ExceptionMessage
            });
        }
    }
}

// ReSharper disable once CheckNamespace
// ReSharper disable InconsistentNaming
namespace Metrics.Logging.Consumers.CommandMetrics
{
    public interface LogCommandMetrics
    {
        Guid CorrelationId { get; }
        DateTime TimeStarted { get; }
        DateTime TimeFinished { get; }
        string CommandTypeName { get; }
        string? ExceptionMessage { get; }
    }
}