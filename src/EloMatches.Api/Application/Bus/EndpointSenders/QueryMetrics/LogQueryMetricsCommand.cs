using System;

namespace EloMatches.Api.Application.Bus.EndpointSenders.QueryMetrics
{
    public class LogQueryMetricsCommand
    {
        public LogQueryMetricsCommand(Guid correlationId, DateTime timeStarted, string queryTypeName)
        {
            CorrelationId = correlationId;
            TimeStarted = timeStarted;
            QueryTypeName = queryTypeName;
        }

        public Guid CorrelationId { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime TimeFinished { get; set; }
        public string QueryTypeName { get; set; }
        public string? ExceptionMessage { get; set; }
    }
}