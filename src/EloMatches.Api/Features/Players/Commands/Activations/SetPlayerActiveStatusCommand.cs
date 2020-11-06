using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Infrastructure.CommandPipeline;

namespace EloMatches.Api.Features.Players.Commands.Activations
{
    public class SetPlayerActiveStatusCommand : ICommand<CommandHandlerResponse>
    {
        public SetPlayerActiveStatusCommand(PlayerId playerId, bool activeStatus)
        {
            PlayerId = playerId;
            ActiveStatus = activeStatus;
        }

        public PlayerId PlayerId { get; }
        public bool ActiveStatus { get; }
    }
}