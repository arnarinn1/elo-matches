using System;
using System.Threading.Tasks;
using EloMatches.Api.Features.Players.Commands.Activations;
using EloMatches.Api.Features.Players.Commands.ChangeEmail;
using EloMatches.Api.Features.Players.Commands.ChangePlayerNames;
using EloMatches.Api.Features.Players.Commands.CreatePlayer;
using EloMatches.Api.Features.Players.Queries.PlayerById;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents;
using EloMatches.Domain.ValueObjects;
using EloMatches.Query.Projections.Players;
using NUnit.Framework;

namespace EloMatches.Tests.Integration.Features.Players
{
    [TestFixture, Category("Integration")]
    internal class PlayerTests : IntegrationTestBase
    {
        private const string AggregateType = "Player";
        private readonly PlayerId _playerId = new PlayerId(new Guid("94A2FE3A-1C6D-47DF-86DB-E235DC3F0EDF"));
        private readonly Name _userName = new Name("arnar.heimisson");
        private readonly Name _displayName = new Name("Arnar");
        private readonly EmailAddress _emailAddress = new EmailAddress("arnar.heimisson@meniga.com");

        [Test]
        public async Task CreatePlayerCommand_ShouldCreatePlayer_WhenPlayerDoesNotExist()
        {
            await Command(new CreatePlayerCommand(_playerId, _userName, _displayName, _emailAddress));

            var player = await Query<PlayerByIdQuery, PlayerProjection>(new PlayerByIdQuery(_playerId));

            var domainEvents = await QueryDomainEvents(_playerId.ToString(), AggregateType);
            
            player.AssertPlayer(_playerId, _userName, _displayName, _emailAddress, true);

            domainEvents.VerifyDomainEventCount(1);
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerCreated>();
        }

        [Test]
        public async Task ChangePlayerNamesCommand_ShouldChangePlayerUserName_WhenUserNameHasChanged()
        {
            var userName = new Name("ChangedUserName");

            await Command(new CreatePlayerCommand(_playerId, _userName, _displayName, _emailAddress));
            await Command(new ChangePlayerNamesCommand(_playerId, userName, _displayName));

            var player = await Query<PlayerByIdQuery, PlayerProjection>(new PlayerByIdQuery(_playerId));

            var domainEvents = await QueryDomainEvents(_playerId.ToString(), AggregateType);

            player.AssertPlayer(_playerId, userName, _displayName, _emailAddress, true);

            domainEvents.VerifyDomainEventCount(2);
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerCreated>();
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerUserNameChanged>();
        }

        [Test]
        public async Task ChangePlayerNamesCommand_ShouldChangePlayerDisplayName_WhenUserDisplayHasChanged()
        {
            var displayName = new Name("ChangedDisplayName");

            await Command(new CreatePlayerCommand(_playerId, _userName, _displayName, _emailAddress));
            await Command(new ChangePlayerNamesCommand(_playerId, _userName, displayName));

            var player = await Query<PlayerByIdQuery, PlayerProjection>(new PlayerByIdQuery(_playerId));

            var domainEvents = await QueryDomainEvents(_playerId.ToString(), AggregateType);

            player.AssertPlayer(_playerId, _userName, displayName, _emailAddress, true);

            domainEvents.VerifyDomainEventCount(2);
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerCreated>();
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerDisplayNameChanged>();
        }

        [Test]
        public async Task ChangePlayerNamesCommand_ShouldNotChangeAnything_WhenNamesHaveNotChanged()
        {
            await Command(new CreatePlayerCommand(_playerId, _userName, _displayName, _emailAddress));
            await Command(new ChangePlayerNamesCommand(_playerId, _userName, _displayName));

            var player = await Query<PlayerByIdQuery, PlayerProjection>(new PlayerByIdQuery(_playerId));

            var domainEvents = await QueryDomainEvents(_playerId.ToString(), AggregateType);

            player.AssertPlayer(_playerId, _userName, _displayName, _emailAddress, true);

            domainEvents.VerifyDomainEventCount(1);
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerCreated>();
        }

        [Test]
        public async Task ChangePlayerEmailCommand_ShouldChangeEmail_WhenEmailHasChanged()
        {
            var emailAddress = new EmailAddress("arnar.heimisson@meniga.net");

            await Command(new CreatePlayerCommand(_playerId, _userName, _displayName, _emailAddress));
            await Command(new ChangePlayerEmailCommand(_playerId, emailAddress));

            var player = await Query<PlayerByIdQuery, PlayerProjection>(new PlayerByIdQuery(_playerId));

            var domainEvents = await QueryDomainEvents(_playerId.ToString(), AggregateType);

            player.AssertPlayer(_playerId, _userName, _displayName, emailAddress, true);

            domainEvents.VerifyDomainEventCount(2);
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerCreated>();
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerEmailChanged>();
        }

        [Test]
        public async Task ChangePlayerEmailCommand_ShouldNotChangeAnything_WhenEmailHasNotChanged()
        {
            await Command(new CreatePlayerCommand(_playerId, _userName, _displayName, _emailAddress));
            await Command(new ChangePlayerEmailCommand(_playerId, _emailAddress));

            var player = await Query<PlayerByIdQuery, PlayerProjection>(new PlayerByIdQuery(_playerId));

            var domainEvents = await QueryDomainEvents(_playerId.ToString(), AggregateType);

            player.AssertPlayer(_playerId, _userName, _displayName, _emailAddress, true);

            domainEvents.VerifyDomainEventCount(1);
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerCreated>();
        }

        [Test]
        public async Task SetPlayerActiveStatusCommand_ShouldDeactivatePlayer_WhenPlayerWasActive()
        {
            await Command(new CreatePlayerCommand(_playerId, _userName, _displayName, _emailAddress));
            await Command(new SetPlayerActiveStatusCommand(_playerId, false));

            var player = await Query<PlayerByIdQuery, PlayerProjection>(new PlayerByIdQuery(_playerId));

            var domainEvents = await QueryDomainEvents(_playerId.ToString(), AggregateType);

            player.AssertPlayer(_playerId, _userName, _displayName, _emailAddress, false);

            domainEvents.VerifyDomainEventCount(2);
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerCreated>();
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerDeactivated>();
        }

        [Test]
        public async Task SetPlayerActiveStatusCommand_ShouldDeactivatePlayer_WhenPlayerWasInactive()
        {
            await Command(new CreatePlayerCommand(_playerId, _userName, _displayName, _emailAddress));
            await Command(new SetPlayerActiveStatusCommand(_playerId, false));
            await Command(new SetPlayerActiveStatusCommand(_playerId, true));

            var player = await Query<PlayerByIdQuery, PlayerProjection>(new PlayerByIdQuery(_playerId));

            var domainEvents = await QueryDomainEvents(_playerId.ToString(), AggregateType);

            player.AssertPlayer(_playerId, _userName, _displayName, _emailAddress, true);

            domainEvents.VerifyDomainEventCount(3);
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerCreated>();
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerDeactivated>();
            domainEvents.VerifyThatSingleDomainEventOccurred<PlayerReactivated>();
        }
    }
}