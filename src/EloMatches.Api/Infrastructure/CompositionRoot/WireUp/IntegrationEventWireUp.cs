using EloMatches.Api.Application.IntegrationEvents;
using EloMatches.Api.Application.IntegrationEvents.Mapping;
using EloMatches.Api.Infrastructure.CompositionRoot.Implementations;
using SimpleInjector;

namespace EloMatches.Api.Infrastructure.CompositionRoot.WireUp
{
    public static class IntegrationEventWireUp
    {
        public static Container RegisterIntegrationEventPipeline(this Container container)
        {
            container.Collection.Register(typeof(ITranslateDomainEventToIntegrationEvent<>), typeof(ITranslateDomainEventToIntegrationEvent<>).Assembly);
            container.RegisterSingleton<IIntegrationEventTranslator, IntegrationEventTranslatorFromContainer>();
            container.RegisterSingleton<IDomainEventToIntegrationEventTranslationCollection, DomainEventToIntegrationEventTranslationCollection>();
            return container;
        }
    }
}