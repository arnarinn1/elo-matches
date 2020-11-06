using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using EloMatches.Infrastructure.CommandPipeline;

namespace EloMatches.Api.Features.Players.Commands.ChangePlayerNames
{
    public class ChangePlayerNamesCommand : ICommand<CommandHandlerResponse>
    {
        public ChangePlayerNamesCommand(PlayerId playerId, Name userName, Name displayName)
        {
            PlayerId = playerId;
            UserName = userName;
            DisplayName = displayName;
        }

        public PlayerId PlayerId { get; }
        public Name UserName { get; }
        public Name DisplayName { get; }
    }
}