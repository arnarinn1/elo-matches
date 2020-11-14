using System;

namespace EloMatches.Api.Application.Bus.EndpointSenders.CommandMetrics
{
    public class LogCommandMetricsCommand
    {
        public LogCommandMetricsCommand(Guid correlationId, DateTime timeStarted, string commandTypeName)
        {
            CorrelationId = correlationId;
            TimeStarted = timeStarted;
            CommandTypeName = commandTypeName;
        }

        public Guid CorrelationId { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime TimeFinished { get; set; }
        public string CommandTypeName { get; set; }
        public string? ExceptionMessage { get; set; }
    }
}