using System;
using System.Collections.Generic;

namespace EloMatches.Domain.ValueObjects
{
    public class EloRatingStatistics : ValueObject
    {
        public decimal CurrentEloRating { get; private set; }
        public decimal AverageEloRating { get; private set; }
        public decimal LowestEloRating { get; private set; }
        public decimal HighestEloRating { get; private set; }
        public decimal EloRatingDifference { get; private set; }

        public EloRatingStatistics() : this(1000, 1000, 1000, 1000, 0) { }

        private EloRatingStatistics(decimal currentEloRating, decimal averageEloRating, decimal lowestEloRating, decimal highestEloRating, decimal eloRatingDifference)
        {
            CurrentEloRating = currentEloRating;
            AverageEloRating = averageEloRating;
            LowestEloRating = lowestEloRating;
            HighestEloRating = highestEloRating;
            EloRatingDifference = eloRatingDifference;
        }

        public EloRatingStatistics AddWin(EloCalculationDifference eloCalculation, int totalGamesPlayed)
        {
            return Create(eloCalculation.EloGainedForWinner, totalGamesPlayed);
        }

        public EloRatingStatistics AddLoss(EloCalculationDifference eloCalculation, int totalGamesPlayed)
        {
            return Create(eloCalculation.EloLossForLoser, totalGamesPlayed);
        }

        private EloRatingStatistics Create(decimal eloRatingForGame, int totalGamesPlayed)
        {
            if (totalGamesPlayed < 1)
                throw new ArgumentException("TotalGamesPlayed must be greater than zero");

            var currentEloRating = CurrentEloRating + eloRatingForGame;

            var averageEloRating = (AverageEloRating * totalGamesPlayed + eloRatingForGame) / totalGamesPlayed;

            var lowestEloRating = currentEloRating < LowestEloRating 
                ? currentEloRating 
                : LowestEloRating;
            
            var highestEloRating = currentEloRating > HighestEloRating 
                ? currentEloRating 
                : HighestEloRating;

            var eloRatingDifference = EloRatingDifference + eloRatingForGame;

            return new EloRatingStatistics(currentEloRating, averageEloRating, lowestEloRating, highestEloRating, eloRatingDifference);
        }

        public override string ToString()
        {
            return $"CurrentEloRating = '{CurrentEloRating}' - AverageEloRating = '{AverageEloRating}' - LowestEloRating = '{LowestEloRating}' - HighestEloRating = '{HighestEloRating} - EloRatingDifference = '{EloRatingDifference}''";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CurrentEloRating;
            yield return AverageEloRating;
            yield return LowestEloRating;
            yield return HighestEloRating;
            yield return EloRatingDifference;
        }
    }
}