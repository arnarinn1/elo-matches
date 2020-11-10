using System;
using System.Threading.Tasks;
using EloMatches.Api.Features.Players.Commands.CreatePlayer;
using EloMatches.Api.Features.Players.Queries.PlayerById;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using EloMatches.Query.Projections.Players;
using FluentAssertions;
using NUnit.Framework;

namespace EloMatches.Tests.Integration.Features
{
    [TestFixture, Category("Integration")]
    internal class PlayerTests : IntegrationTestBase
    {
        private readonly PlayerId _playerId = new PlayerId(new Guid("94A2FE3A-1C6D-47DF-86DB-E235DC3F0EDF"));
        private readonly Name _userName = new Name("arnar.heimisson");
        private readonly Name _displayName = new Name("Arnar");
        private readonly EmailAddress _emailAddress = new EmailAddress("arnar.heimisson@meniga.com");

        [Test]
        public async Task CreatePlayerCommand_ShouldCreatePlayer_WhenPlayerDoesNotExist()
        {
            await Command(new CreatePlayerCommand(_playerId, _userName, _displayName, _emailAddress));

            var player = await Query<PlayerByIdQuery, PlayerProjection>(new PlayerByIdQuery(_playerId));

            player.PlayerId.Should().Be(_playerId.Id);
            player.UserName.Should().Be(_userName.Value);
            player.DisplayName.Should().Be(_displayName.Value);
            player.Email.Should().Be(_emailAddress.Value);
            player.ActiveSince.Should().NotBeNull();
            player.DeactivatedSince.Should().BeNull();
            player.IsActive.Should().BeTrue();
        }
    }
}