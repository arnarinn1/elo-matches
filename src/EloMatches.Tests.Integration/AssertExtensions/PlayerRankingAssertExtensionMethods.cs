using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Query.Projections.PlayerRankings;
using FluentAssertions;

namespace EloMatches.Tests.Integration.AssertExtensions
{
    public static class PlayerRankingAssertExtensionMethods
    {
        public static void AssertPlayerRankingWithNoGamesPlayed(this PlayerRankingProjection player, PlayerId playerId)
        {
            player.Id.Should().Be(playerId.Id);
            player.CurrentEloRating.Should().Be(1000);
            player.AverageEloRating.Should().Be(1000);
            player.HighestEloRating.Should().Be(1000);
            player.LowestEloRating.Should().Be(1000);
            player.PlayedGames.Should().Be(0);
        }
    }
}