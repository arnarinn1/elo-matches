using System;
using System.Collections.Generic;
using System.Linq;
using EloMatches.Domain.SeedWork;
using EloMatches.Shared.Extensions;

namespace EloMatches.Api.Application.IntegrationEvents.Mapping
{
    public class DomainEventToIntegrationEventTranslationCollection : IDomainEventToIntegrationEventTranslationCollection
    {
        private Dictionary<Type, bool>? _mapping;
        public Dictionary<Type, bool> Mapping
        {
            get
            {
                if (_mapping != null)
                    return _mapping;

                _mapping = new Dictionary<Type, bool>();

                var domainEvents = ReflectionHelpers.GetAllTypesImplementingInterface(typeof(IDomainEvent), typeof(IDomainEvent).Assembly);

                var integrationEventTranslations = ReflectionHelpers.GetAllTypesImplementingOpenGenericType(typeof(ITranslateDomainEventToIntegrationEvent<>), typeof(Startup).Assembly).ToArray();

                foreach (var domainEvent in domainEvents)
                {
                    var hasTranslations = integrationEventTranslations.Any(x => x.GetInterfaces().Any(y => typeof(ITranslateDomainEventToIntegrationEvent<>).MakeGenericType(domainEvent).IsAssignableFrom(y)));
                    _mapping.Add(domainEvent, hasTranslations);
                }

                return _mapping;
            }
        }
    }
}