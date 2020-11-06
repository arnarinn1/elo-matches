using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using EloMatches.Infrastructure.CommandPipeline;

namespace EloMatches.Api.Features.Players.Commands.ChangeEmail
{
    public class ChangePlayerEmailCommand : ICommand<CommandHandlerResponse>
    {
        public ChangePlayerEmailCommand(PlayerId playerId, EmailAddress email)
        {
            PlayerId = playerId;
            Email = email;
        }

        public PlayerId PlayerId { get; }
        public EmailAddress Email { get; }
    }
}