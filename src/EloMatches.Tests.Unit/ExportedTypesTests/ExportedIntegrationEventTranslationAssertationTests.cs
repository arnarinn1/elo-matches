using System;
using System.Collections.Generic;
using System.Linq;
using EloMatches.Api;
using EloMatches.Api.Application.IntegrationEvents;
using EloMatches.Api.Application.IntegrationEvents.Translations.Players;
using EloMatches.Domain.AggregateModels.MatchesAggregate.DomainEvents;
using EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents;
using EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate.DomainEvents;
using EloMatches.Domain.AggregateModels.PlayerRankingAggregate.DomainEvents;
using EloMatches.Domain.SeedWork;
using EloMatches.Shared.Extensions;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.ExportedTypesTests
{
    [TestFixture, Category("Unit")]
    public class ExportedIntegrationEventTranslationAssertationTests
    {
        private static readonly IDictionary<Type, ISet<Type>> DomainEvents =
           new Dictionary<Type, ISet<Type>>
           {
                //PlayerAggregate
                {typeof(PlayerCreated), new HashSet<Type>{typeof(PlayerCreatedTranslation)}},
                {typeof(PlayerUserNameChanged), new HashSet<Type>()},
                {typeof(PlayerDisplayNameChanged), new HashSet<Type>()},
                {typeof(PlayerEmailChanged), new HashSet<Type>()},
                {typeof(PlayerReactivated), new HashSet<Type>()},
                {typeof(PlayerDeactivated), new HashSet<Type>()},

                //PlayerRankingAggregate
                {typeof(PlayerRankingCreated), new HashSet<Type>()},
                {typeof(WinAddedToPlayerRanking), new HashSet<Type>()},
                {typeof(LossAddedToPlayerRanking), new HashSet<Type>()},

                //PlayerRankingAggregate
                {typeof(PlayerAddedToLeaderBoard), new HashSet<Type>()},
                {typeof(PlayerDescendedOnLeaderBoard), new HashSet<Type>()},
                {typeof(PlayerAscendedOnLeaderBoard), new HashSet<Type>()},

                //MatchesAfterReferenceDate
                {typeof(MatchRegistered), new HashSet<Type>()}
           };

        [Test]
        public void IntegrationEventTranslationTest()
        {
            var domainEvents = ReflectionHelpers.GetAllTypesImplementingInterface(typeof(IDomainEvent), typeof(IDomainEvent).Assembly);

            var integrationEventTranslations = ReflectionHelpers.GetAllTypesImplementingOpenGenericType(typeof(ITranslateDomainEventToIntegrationEvent<>), typeof(Startup).Assembly).ToArray();

            foreach (var domainEvent in domainEvents)
            {
                if (!DomainEvents.TryGetValue(domainEvent, out var domainEventMap))
                    Assert.Fail($"Missing IntegrationEventTranslation mapping for DomainEvent = '{domainEvent.Name}'");

                var registeredTranslations = integrationEventTranslations.Where(x => x.GetInterfaces().Any(y => typeof(ITranslateDomainEventToIntegrationEvent<>).MakeGenericType(domainEvent).IsAssignableFrom(y))).ToArray();

                foreach (var translation in registeredTranslations)
                {
                    if (!domainEventMap.Contains(translation))
                        Assert.Fail($"Found handler = '{translation.Name}' that is incorrectly registered in test setup for DomainEvent = '{domainEvent.Name}'");
                }
            }
        }
    }
}