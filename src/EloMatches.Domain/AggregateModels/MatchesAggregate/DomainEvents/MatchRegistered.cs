using EloMatches.Domain.SeedWork;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.MatchesAggregate.DomainEvents
{
    public class MatchRegistered : DomainEvent
    {
        public MatchRegistered(MatchDate matchDate, MatchResult matchResult, EloCalculationDifference eloCalculationDifference, PlayerMatchResult winner, PlayerMatchResult loser) : base(matchDate.ToString(), "MatchesAfterReferenceDate")
        {
            MatchDate = matchDate;
            MatchResult = matchResult;
            EloCalculationDifference = eloCalculationDifference;
            Winner = winner;
            Loser = loser;
        }

        public MatchDate MatchDate { get; }
        public MatchResult MatchResult { get; }
        public EloCalculationDifference EloCalculationDifference { get; }
        public PlayerMatchResult Winner { get; }
        public PlayerMatchResult Loser { get; }
    }
}