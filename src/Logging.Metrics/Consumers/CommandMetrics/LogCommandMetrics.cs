#nullable enable
using System;

namespace Logging.Metrics.Consumers.CommandMetrics
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