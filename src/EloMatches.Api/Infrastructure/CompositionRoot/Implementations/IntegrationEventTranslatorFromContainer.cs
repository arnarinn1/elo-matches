using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EloMatches.Api.Application.IntegrationEvents;
using EloMatches.Api.Application.IntegrationEvents.Mapping;
using EloMatches.Domain.SeedWork;
using EloMatches.IntegrationEvents.SeedWork;
using EloMatches.Shared.SeedWork;

namespace EloMatches.Api.Infrastructure.CompositionRoot.Implementations
{
    public class IntegrationEventTranslatorFromContainer : IIntegrationEventTranslator
    {
        private readonly IResolverFactory _resolverFactory;
        private readonly IDomainEventToIntegrationEventTranslationCollection _translationCollection;

        private static readonly IReadOnlyCollection<IIntegrationEvent> EmptyList = new List<IIntegrationEvent>();

        public IntegrationEventTranslatorFromContainer(IResolverFactory resolverFactory, IDomainEventToIntegrationEventTranslationCollection translationCollection)
        {
            _resolverFactory = resolverFactory ?? throw new ArgumentNullException(nameof(resolverFactory));
            _translationCollection = translationCollection ?? throw new ArgumentNullException(nameof(translationCollection));
        }

        public async Task<IReadOnlyCollection<IIntegrationEvent>> Translate<T>(T domainEvent) where T : IDomainEvent
        {
            if (!_translationCollection.HasIntegrationEventTranslations(domainEvent))
                return EmptyList;

            var instances = _resolverFactory.ResolveInstances(typeof(ITranslateDomainEventToIntegrationEvent<>).MakeGenericType(domainEvent.GetType()));
            
            var integrationEvents = new List<IIntegrationEvent>();
            
            foreach (dynamic instance in instances)
            {
                IIntegrationEvent integrationEvent = await instance.Translate((dynamic) domainEvent);
                integrationEvents.Add(integrationEvent);
            }

            return integrationEvents;
        }
    }
}