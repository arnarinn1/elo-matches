using System;
using System.Collections.Generic;
using System.Linq;
using EloMatches.Api;
using EloMatches.Api.Features.Matches.DomainEventHandlers;
using EloMatches.Api.Features.Players.DomainEventHandlers;
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
    public class DomainEventHandlerCollectionAssertationTests
    {
        private static readonly IDictionary<Type, ISet<Type>> DomainEvents =
            new Dictionary<Type, ISet<Type>>
            {
                //PlayerAggregate
                {typeof(PlayerCreated), new HashSet<Type>{typeof(CreatePlayerRankingOnPlayerCreatedEventHandler)}},
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
                {typeof(MatchRegistered), new HashSet<Type>{typeof(UpdatePlayerRankingOnMatchRegisteredEventHandler), typeof(UpdatePlayerLeaderBoardOnMatchRegisteredEventHandler) }}
            };

        [Test]
        public void DomainEventHandlersValidation()
        {
            var domainEvents = ReflectionHelpers.GetAllTypesImplementingInterface(typeof(IDomainEvent), typeof(IDomainEvent).Assembly);

            var domainEventHandlers = ReflectionHelpers.GetAllTypesImplementingOpenGenericType(typeof(IDomainEventHandler<>), typeof(Startup).Assembly).ToArray();

            foreach (var domainEvent in domainEvents)
            {
                if (!DomainEvents.TryGetValue(domainEvent, out var domainEventMap))
                    Assert.Fail($"Missing DomainEventHandler mapping for DomainEvent = '{domainEvent.Name}'");

                var registeredDomainEventHandlers = domainEventHandlers.Where(x => x.GetInterfaces().Any(y => typeof(IDomainEventHandler<>).MakeGenericType(domainEvent).IsAssignableFrom(y))).ToArray();

                foreach (var handler in registeredDomainEventHandlers)
                {
                    if (!domainEventMap.Contains(handler))
                        Assert.Fail($"Found handler = '{handler.Name}' that is incorrectly registered in test setup for DomainEvent = '{domainEvent.Name}'");
                }
            }
        }
    }
}