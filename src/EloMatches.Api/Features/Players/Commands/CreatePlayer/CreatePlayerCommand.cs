using EloMatches.Api.Features.Players.Commands.CreatePlayer.HttpPayloads;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using EloMatches.Infrastructure.CommandPipeline;

namespace EloMatches.Api.Features.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommand : ICommand<CreatePlayerResponseBody>
    {
        public CreatePlayerCommand(PlayerId playerId, Name userName, Name displayName, EmailAddress email)
        {
            PlayerId = playerId;
            UserName = userName;
            DisplayName = displayName;
            Email = email;
        }

        public PlayerId PlayerId { get; }
        public Name UserName { get; }
        public Name DisplayName { get; }
        public EmailAddress Email { get; }
    }
}