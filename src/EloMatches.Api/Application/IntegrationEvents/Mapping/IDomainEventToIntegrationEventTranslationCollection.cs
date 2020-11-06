using System;
using System.Collections.Generic;

namespace EloMatches.Api.Application.IntegrationEvents.Mapping
{
    public interface IDomainEventToIntegrationEventTranslationCollection
    {
        Dictionary<Type, bool> Mapping { get; }
    }
}