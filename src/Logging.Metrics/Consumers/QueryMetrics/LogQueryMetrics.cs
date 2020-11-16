#nullable enable
using System;

namespace Metrics.Logging.Consumers.QueryMetrics
{
    // ReSharper disable once InconsistentNaming
    public interface LogQueryMetrics
    {
        Guid CorrelationId { get; }
        DateTime TimeStarted { get; }
        DateTime TimeFinished { get; }
        string QueryTypeName { get; }
        string? ExceptionMessage { get; }
    }
}