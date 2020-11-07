using System;
using System.Collections.Generic;
using EloMatches.Domain.AggregateModels.MatchesAggregate.DomainEvents;
using EloMatches.Domain.SeedWork;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.MatchesAggregate
{
    public class MatchesAfterReferenceDate : Aggregate, IAggregateRoot
    {
        private MatchDate ReferenceDate { get; set; }
        private ICollection<Match> AllMatchesRegisteredAfterMatchDate { get; set; }
        private Match RegisteredMatch { get; set; }
        
        private PlayerStatsBeforeMatch Winner { get; set; }
        private PlayerStatsBeforeMatch Loser { get; set; }

        private MatchesAfterReferenceDate(MatchDate referenceDate, PlayerStatsBeforeMatch winner, PlayerStatsBeforeMatch loser, ICollection<Match> allMatchesRegisteredAfterMatchDate)
        {
            if (winner == loser)
                throw new ArgumentException($"WinnerId and LoserId can't be the same id, a player can't play against himself. Id = '{winner.PlayerId}'");

            ReferenceDate = referenceDate;
            Winner = winner;
            Loser = loser;
            AllMatchesRegisteredAfterMatchDate = allMatchesRegisteredAfterMatchDate;
        }

        public int GetRegisteredMatchId() => RegisteredMatch.Id;

        public void RegisterMatch(MatchResult matchResult, MatchDate matchDate)
        {
            if (matchDate.IsEarlierThan(ReferenceDate))
                throw new ArgumentException("MatchDate can't be an earlier date than ReferenceDate");

            var eloCalculation = new EloCalculationDifference(Winner.EloRating, Loser.EloRating);

            var winnerResult = PlayerMatchResult.Win(Winner, eloCalculation);
            var loserResult = PlayerMatchResult.Loss(Loser, eloCalculation);

            RegisteredMatch = new Match(matchResult, winnerResult, loserResult, matchDate);
            
            //todo -> RecalculateMatches if there are any.

            AddDomainEvent(new MatchRegistered(matchDate, matchResult, eloCalculation, winnerResult, loserResult));
        }
    }
}