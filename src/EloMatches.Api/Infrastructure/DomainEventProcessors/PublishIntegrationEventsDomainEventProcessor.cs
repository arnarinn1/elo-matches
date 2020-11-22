using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EloMatches.Api.Application.IntegrationEvents;
using EloMatches.Domain.SeedWork;
using MassTransit;

namespace EloMatches.Api.Infrastructure.DomainEventProcessors
{
    public class PublishIntegrationEventsDomainEventProcessor : IDomainEventProcessor
    {
        private readonly IIntegrationEventTranslator _translator;
        private readonly IBusControl _busControl;

        public PublishIntegrationEventsDomainEventProcessor(IIntegrationEventTranslator resolver, IBusControl busControl)
        {
            _translator = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
        }

        public async Task Process(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                var integrationEvents = await _translator.Translate(domainEvent);

                foreach (var integrationEvent in integrationEvents)
                {
                    var eventAsObject = integrationEvent as object;
                    await _busControl.Publish(eventAsObject);
                }
            }
        }
    }
}