using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EloMatches.Domain.AggregateModels.MatchesAggregate;
using EloMatches.Domain.AggregateModels.MatchesAggregate.DomainEvents;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.Domain.AggregateModels.MatchesAggregate
{
    [TestFixture, Category("Unit")]
    public class MatchesAfterReferenceDateTests
    {
        private readonly PlayerId _playerIdOne = new PlayerId(new Guid("F313D230-B8F6-46D9-A072-CA005DDA0460"));
        private readonly PlayerId _playerIdTwo = new PlayerId(new Guid("659E7452-47D6-4AB1-B12D-5B21DFD26FE6"));

        [Test]
        public void RegisterMatch_ShouldThrowArgumentException_WhenMatchDateIsAnEarlierDateThanReferenceDate()
        {
            var winner = new PlayerStatsBeforeMatch(_playerIdOne, 1000, 0);
            var loser = new PlayerStatsBeforeMatch(_playerIdTwo, 1000, 0);

            var matches = MatchesReflectionFactory.Create(new MatchDate(10.November(2020)), winner, loser,new List<Match>());

            Action action = () => matches.RegisterMatch(new MatchResult(11, 6), new MatchDate(9.November(2020)));

            action.Should().Throw<ArgumentException>().Which.Message.Should().Be("MatchDate can't be an earlier date than ReferenceDate");
        }

        [Test]
        public void RegisterMatch_ShouldAddMatchAndNotRecalculate_WhenThereAreNoMatchesAfterReferenceDate()
        {
            var winner = new PlayerStatsBeforeMatch(_playerIdOne, 1000, 0);
            var loser = new PlayerStatsBeforeMatch(_playerIdTwo, 1000, 0);

            var matches = MatchesReflectionFactory.Create(new MatchDate(10.November(2020)), winner, loser, new List<Match>());

            matches.RegisterMatch(new MatchResult(11, 5), new MatchDate(10.November(2020).At(12, 00)));

            var constraint = matches.DomainEvents.Single().Should().BeOfType<MatchRegistered>();
            
            constraint.Subject.Winner.PlayerId.Should().Be(_playerIdOne.Id);
            constraint.Subject.Loser.PlayerId.Should().Be(_playerIdTwo.Id);
            constraint.Subject.MatchResult.Should().Be(new MatchResult(11, 5));
        }

        private static class MatchesReflectionFactory
        {
            private static Func<MatchDate, PlayerStatsBeforeMatch, PlayerStatsBeforeMatch, ICollection<Match>, MatchesAfterReferenceDate> CreateInstanceFactory { get; }
            private static Func<MatchesAfterReferenceDate, Match> GetRegisteredMatchFactory { get; }

            static MatchesReflectionFactory()
            {
                var constructor = typeof(MatchesAfterReferenceDate).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single();

                CreateInstanceFactory = (matchDate, winnerStats, loserStats, allMatches) =>
                {
                    return (MatchesAfterReferenceDate)constructor.Invoke(new object[] { matchDate, winnerStats, loserStats, allMatches });
                };

                var propertyForRegisteredMatches = typeof(MatchesAfterReferenceDate).GetProperty("RegisteredMatch", BindingFlags.Instance | BindingFlags.NonPublic);

                if (propertyForRegisteredMatches == null)
                    throw new ArgumentNullException(nameof(propertyForRegisteredMatches));

                GetRegisteredMatchFactory = matches => (Match)propertyForRegisteredMatches.GetValue(matches);
            }

            internal static MatchesAfterReferenceDate Create(MatchDate referenceDate, PlayerStatsBeforeMatch winnerStats, PlayerStatsBeforeMatch loserStats, ICollection<Match> allMatchesRegisteredAfterMatchDate)
            {
                return CreateInstanceFactory(referenceDate, winnerStats, loserStats, allMatchesRegisteredAfterMatchDate);
            }

            internal static Match GetRegisteredMatch(MatchesAfterReferenceDate aggregate) => GetRegisteredMatchFactory(aggregate);
        }
    }
}