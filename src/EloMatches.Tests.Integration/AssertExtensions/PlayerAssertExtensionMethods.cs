using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using EloMatches.Query.Projections.Players;
using FluentAssertions;

namespace EloMatches.Tests.Integration.AssertExtensions
{
    public static class PlayerAssertExtensionMethods
    {
        public static void AssertPlayer(this PlayerProjection player, PlayerId playerId, Name userName, Name displayName, EmailAddress emailAddress, bool shouldBeActive)
        {
            player.PlayerId.Should().Be(playerId.Id);
            player.UserName.Should().Be(userName.Value);
            player.DisplayName.Should().Be(displayName.Value);
            player.Email.Should().Be(emailAddress.Value);

            if (shouldBeActive)
            {
                player.ActiveSince.Should().NotBeNull();
                player.DeactivatedSince.Should().BeNull();
                player.IsActive.Should().BeTrue();
            }
            else
            {
                player.ActiveSince.Should().BeNull();
                player.DeactivatedSince.Should().NotBeNull();
                player.IsActive.Should().BeFalse();
            }
        }
    }
}