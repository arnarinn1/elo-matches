using System.Collections.Generic;
using System.Threading.Tasks;
using EloMatches.Domain.SeedWork;
using EloMatches.IntegrationEvents.SeedWork;

namespace EloMatches.Api.Application.IntegrationEvents
{
    public interface IIntegrationEventTranslator
    {
        Task<IReadOnlyCollection<IIntegrationEvent>> Translate<T>(T domainEvent) where T : IDomainEvent;
    }
}