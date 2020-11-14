using System;

namespace Logging.Metrics.Consumers.CommandMetrics
{
    // ReSharper disable once InconsistentNaming
    public interface LogCommandMetrics
    {
        Guid Identifier { get; }
        DateTime TimeStarted { get; }
        DateTime TimeFinished { get; }
        string CommandTypeName { get; }
    }
}