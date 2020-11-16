#nullable enable
using System;

namespace Metrics.Logging.Consumers.CommandMetrics
{
    // ReSharper disable once InconsistentNaming
    public interface LogCommandMetrics
    {
        Guid CorrelationId { get; }
        DateTime TimeStarted { get; }
        DateTime TimeFinished { get; }
        string CommandTypeName { get; }
        string? ExceptionMessage { get; }
    }
}