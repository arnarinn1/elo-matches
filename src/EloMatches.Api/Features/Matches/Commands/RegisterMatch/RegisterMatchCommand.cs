using EloMatches.Api.Features.Matches.Commands.RegisterMatch.HttpPayloads;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using EloMatches.Infrastructure.CommandPipeline;

namespace EloMatches.Api.Features.Matches.Commands.RegisterMatch
{
    public class RegisterMatchCommand : ICommand<RegisterMatchResponseBody>
    {
        public RegisterMatchCommand(PlayerId winnerPlayerId, PlayerId loserPlayerId, MatchResult matchResult, MatchDate matchDate)
        {
            WinnerPlayerId = winnerPlayerId;
            LoserPlayerId = loserPlayerId;
            MatchResult = matchResult;
            MatchDate = matchDate;
        }

        public PlayerId WinnerPlayerId { get; }
        public PlayerId LoserPlayerId { get; }

        public MatchResult MatchResult { get; }
        public MatchDate MatchDate { get; }
    }
}