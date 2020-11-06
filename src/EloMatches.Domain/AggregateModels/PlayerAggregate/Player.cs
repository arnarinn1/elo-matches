using System;
using EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents;
using EloMatches.Domain.SeedWork;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.PlayerAggregate
{
    public class Player : Aggregate, IAggregateRoot
    {
        public Player(PlayerId playerId, Name userName, Name displayName, EmailAddress email)
        {
            Id = playerId ?? throw new ArgumentNullException(nameof(playerId), "PlayerId can't be null when creating Player");
            UserName = userName ?? throw new ArgumentNullException(nameof(userName), "UserName can't be null when creating Player");
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName), "DisplayName can't be null when creating Player");
            Email = email ?? throw new ArgumentNullException(nameof(email), "Email can't be null when creating Player");
            ActiveStatus = PlayerActiveStatus.Active();
            EntryDate = SystemTime.Now();

            AddDomainEvent(new PlayerCreated(playerId, userName, displayName, email));
        }

        private Player() {}

        private Guid Id { get; set; }

        private Name UserName { get; set; }
        private Name DisplayName { get; set; }

        private EmailAddress Email { get; set; }

        private PlayerActiveStatus ActiveStatus { get; set; }

        private DateTime EntryDate { get; set; }

        public void ChangeUserName(Name userName)
        {
            if (userName == UserName)
                return;

            var previousUserName = UserName;
            UserName = userName;

            AddDomainEvent(new PlayerUserNameChanged(new PlayerId(Id), userName, previousUserName));
        }

        public void ChangeDisplayName(Name displayName)
        {
            if (displayName == DisplayName)
                return;

            var previousDisplayName = DisplayName;
            DisplayName = displayName;

            AddDomainEvent(new PlayerDisplayNameChanged(new PlayerId(Id), displayName, previousDisplayName));
        }

        public void ChangeEmail(EmailAddress email)
        {
            if (email == Email)
                return;

            var previousEmail = Email;
            Email = email;

            AddDomainEvent(new PlayerEmailChanged(new PlayerId(Id), email, previousEmail));
        }

        public void Reactivate()
        {
            if (ActiveStatus.ActiveSince != null)
                return;

            var hadBeenDeactivatedSince = ActiveStatus.DeactivatedSince.GetValueOrDefault();
            ActiveStatus = PlayerActiveStatus.Active();

            AddDomainEvent(new PlayerReactivated(new PlayerId(Id), ActiveStatus, hadBeenDeactivatedSince));
        }

        public void Deactivate()
        {
            if (ActiveStatus.DeactivatedSince != null)
                return;

            var hadBeenActiveSince = ActiveStatus.ActiveSince.GetValueOrDefault();
            ActiveStatus = PlayerActiveStatus.Inactive();

            AddDomainEvent(new PlayerDeactivated(new PlayerId(Id), ActiveStatus, hadBeenActiveSince));
        }
    }
}