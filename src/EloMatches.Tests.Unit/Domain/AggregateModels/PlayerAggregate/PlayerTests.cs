using System;
using System.Linq;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents;
using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.Domain.AggregateModels.PlayerAggregate
{
    [TestFixture, Category("Unit")]
    public class PlayerTests
    {
        private static readonly Guid Guid = new Guid("FAD3DC6A-DEE8-4DB5-90F9-29145655C9FF");
        private static readonly PlayerId PlayerId = new PlayerId(Guid);
        private static readonly Name UserName = new Name("UserName");
        private static readonly Name DisplayName = new Name("DisplayName");
        private static readonly EmailAddress Email = new EmailAddress("email@email.com");

        [TearDown]
        public void RunAfterEachTest()
        {
            SystemTime.Reset();
        }

        [Test]
        public void Construction_ShouldThrowArgumentNullException_WhenAnyFieldIsNull()
        {
            Action playerIdIsNull = () => new Player(null, UserName, DisplayName, Email);
            Action userNameIsNull = () => new Player(PlayerId, null, DisplayName, Email);
            Action displayNameIsNull = () => new Player(PlayerId, UserName, null, Email);
            Action emailIsNull = () => new Player(PlayerId, UserName, DisplayName, null);

            playerIdIsNull.Should().Throw<ArgumentNullException>().And.Message.Should().BeEquivalentTo("PlayerId can't be null when creating Player (Parameter 'playerId')");
            userNameIsNull.Should().Throw<ArgumentNullException>().And.Message.Should().BeEquivalentTo("UserName can't be null when creating Player (Parameter 'userName')");
            displayNameIsNull.Should().Throw<ArgumentNullException>().And.Message.Should().BeEquivalentTo("DisplayName can't be null when creating Player (Parameter 'displayName')");
            emailIsNull.Should().Throw<ArgumentNullException>().And.Message.Should().BeEquivalentTo("Email can't be null when creating Player (Parameter 'email')");
        }

        [Test]
        public void Construction_ShouldCreateInstance_WhenAllParametersAreValid()
        {
            var player = new Player(PlayerId, UserName, DisplayName, Email);

            player.Should().NotBeNull();

            var constraint = player.DomainEvents.Single().Should().BeOfType<PlayerCreated>();
            constraint.Which.PlayerId.Should().BeEquivalentTo(new PlayerId(Guid));
            constraint.Which.UserName.Should().BeEquivalentTo(new Name("UserName"));
            constraint.Which.DisplayName.Should().BeEquivalentTo(new Name("DisplayName"));
            constraint.Which.Email.Should().BeEquivalentTo(new EmailAddress("email@email.com"));
        }

        [Test]
        public void ChangeName_ShouldAddDomainEvent_WhenUserNameNameHasChanged()
        {
            var player = CreatePlayerWithoutEvents(userName: new Name("PreviousName"));

            player.ChangeUserName(new Name("ChangedName"));

            var constraint = player.DomainEvents.Single().Should().BeOfType<PlayerUserNameChanged>();
            constraint.Which.PreviousUserName.Should().BeEquivalentTo(new Name("PreviousName"));
            constraint.Which.UserName.Should().BeEquivalentTo(new Name("ChangedName"));
        }

        [Test]
        public void ChangeUserName_ShouldNotAddDomainEvent_WhenUserNameHasNotChanged()
        {
            var player = CreatePlayerWithoutEvents();
            player.ChangeUserName(UserName);
            player.DomainEvents.Should().BeEmpty();
        }

        [Test]
        public void ChangeDisplayName_ShouldAddDomainEvent_WhenDisplayNameHasChanged()
        {
            var player = CreatePlayerWithoutEvents(displayName: new Name("PreviousName"));

            player.ChangeDisplayName(new Name("ChangedName"));

            var constraint = player.DomainEvents.Single().Should().BeOfType<PlayerDisplayNameChanged>();
            constraint.Which.PreviousDisplayName.Should().BeEquivalentTo(new Name("PreviousName"));
            constraint.Which.DisplayName.Should().BeEquivalentTo(new Name("ChangedName"));
        }

        [Test]
        public void ChangeDisplayName_ShouldNotAddDomainEvent_WhenDisplayNameHasNotChanged()
        {
            var player = CreatePlayerWithoutEvents();
            player.ChangeDisplayName(DisplayName);
            player.DomainEvents.Should().BeEmpty();
        }

        [Test]
        public void ChangeEmail_ShouldAddDomainEvent_WhenEmailHasChanged()
        {
            var player = CreatePlayerWithoutEvents(email: new EmailAddress("email@email.com"));

            player.ChangeEmail(new EmailAddress("changed-email@email.com"));

            var constraint = player.DomainEvents.Single().Should().BeOfType<PlayerEmailChanged>();
            constraint.Which.PreviousEmail.Should().BeEquivalentTo(new EmailAddress("email@email.com"));
            constraint.Which.Email.Should().BeEquivalentTo(new EmailAddress("changed-email@email.com"));
        }

        [Test]
        public void ChangeEmail_ShouldNotAddDomainEvent_WhenEmailNotChanged()
        {
            var player = CreatePlayerWithoutEvents();
            player.ChangeEmail(Email);
            player.DomainEvents.Should().BeEmpty();
        }

        [Test]
        public void Deactivate_ShouldAddDomainEvent_WhenPlayerWasActive()
        {
            SystemTime.Now = () => 1.January(2021);

            var player = CreatePlayerWithoutEvents();
            player.Deactivate();

            var constraint = player.DomainEvents.Single().Should().BeOfType<PlayerDeactivated>();
            constraint.Which.ActiveStatus.DeactivatedSince.Should().Be(1.January(2021));
            constraint.Which.ActiveStatus.ActiveSince.Should().BeNull();
            constraint.Which.HadBeenActiveSince.Should().Be(1.January(2021));
        }

        [Test]
        public void Reactivate_ShouldNotAddDomainEvent_WhenPlayerWasActive()
        {
            var player = CreatePlayerWithoutEvents();
            player.Reactivate();
            player.DomainEvents.Should().BeEmpty();
        }

        [Test]
        public void Reactivate_ShouldAddDomainEvent_WhenPlayerWasDeactivated()
        {
            SystemTime.Now = () => 1.January(2021);

            var player = CreatePlayerWithoutEvents();
            player.Deactivate();
            player.ClearDomainEvents();

            player.Reactivate();

            var constraint = player.DomainEvents.Single().Should().BeOfType<PlayerReactivated>();
            constraint.Which.ActiveStatus.ActiveSince.Should().Be(1.January(2021));
            constraint.Which.ActiveStatus.DeactivatedSince.Should().BeNull(); ;
            constraint.Which.HadBeenDeactivatedSince.Should().Be(1.January(2021));
        }

        private static Player CreatePlayerWithoutEvents(PlayerId playerId = null, Name userName = null, Name displayName = null, EmailAddress email = null)
        {
            var player = new Player(playerId ?? PlayerId, userName ?? UserName, displayName ?? DisplayName, email ?? Email);
            player.ClearDomainEvents();
            return player;
        }
    }
}