using EloMatches.Domain.SeedWork;

namespace EloMatches.Api.Application.IntegrationEvents.Mapping
{
    // ReSharper disable once InconsistentNaming
    public static class IDomainEventToIntegrationEventTranslationCollectionExtensions
    {
        public static bool HasIntegrationEventTranslations(this IDomainEventToIntegrationEventTranslationCollection self, IDomainEvent domainEvent)
        {
            self.Mapping.TryGetValue(domainEvent.GetType(), out var value);
            return value;
        }
    }
}