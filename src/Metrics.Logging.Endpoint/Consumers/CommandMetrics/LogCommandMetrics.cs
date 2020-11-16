#nullable enable
using System;

// ReSharper disable CheckNamespace
namespace Metrics.Logging.CommandMetrics
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