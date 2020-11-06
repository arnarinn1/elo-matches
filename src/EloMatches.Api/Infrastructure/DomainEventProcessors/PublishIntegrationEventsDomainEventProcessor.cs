using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EloMatches.Api.Application.IntegrationEvents;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Api.Infrastructure.DomainEventProcessors
{
    public class PublishIntegrationEventsDomainEventProcessor : IDomainEventProcessor
    {
        private readonly IIntegrationEventTranslator _translator;

        public PublishIntegrationEventsDomainEventProcessor(IIntegrationEventTranslator resolver)
        {
            _translator = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public async Task Process(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                var integrationEvents = await _translator.Translate(domainEvent);
                
                //todo->Publish events using MassTransit.
            }
        }
    }
}