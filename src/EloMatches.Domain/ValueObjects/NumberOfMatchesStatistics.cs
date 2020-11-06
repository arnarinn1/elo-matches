using System;
using System.Collections.Generic;

namespace EloMatches.Domain.ValueObjects
{
    public class NumberOfMatchesStatistics : ValueObject
    {
        public int PlayedGames { get; private set; }
        public int Wins { get; private set; }
        public int Losses { get; private set; }
        public int Differential { get; private set; }
        public decimal WinPercentage { get; private set; }

        public NumberOfMatchesStatistics() : this(0, 0, 0) { }

        private NumberOfMatchesStatistics(int playedGames, int wins, int losses)
        {
            PlayedGames = playedGames;
            Wins = wins;
            Losses = losses;
            Differential = wins - losses;
            WinPercentage = Math.Round(playedGames == 0 ? 0.0m : wins / (decimal) playedGames, 4);
        }

        public NumberOfMatchesStatistics AddWin()
        {
            return new NumberOfMatchesStatistics(PlayedGames + 1, Wins + 1, Losses);
        }

        public NumberOfMatchesStatistics AddLoss()
        {
            return new NumberOfMatchesStatistics(PlayedGames + 1, Wins, Losses + 1);
        }

        public override string ToString()
        {
            return $"PlayedGames = '{PlayedGames}' - Wins = '{Wins}' - Losses = '{Losses} - Differential = '{Differential}' - WinPercentage = '{WinPercentage:P}''";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Wins;
            yield return Losses;
        }
    }
}