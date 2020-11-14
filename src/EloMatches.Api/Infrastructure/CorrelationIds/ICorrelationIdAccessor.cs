using System;

namespace EloMatches.Api.Infrastructure.CorrelationIds
{
    public interface ICorrelationIdAccessor
    {
        Guid GetCorrelationId();
    }
}