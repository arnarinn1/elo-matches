using System;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.MatchesAggregate
{
    public class PlayerMatchInformation
    {
        private PlayerMatchInformation(Guid playerId, decimal eloRatingGameDifference, decimal totalEloRatingBeforeGame, decimal totalEloRatingAfterGame, decimal chanceOfWinning, int gameNumber)
        {
            PlayerId = playerId;
            EloRatingGameDifference = eloRatingGameDifference;
            TotalEloRatingBeforeGame = totalEloRatingBeforeGame;
            TotalEloRatingAfterGame = totalEloRatingAfterGame;
            ChanceOfWinning = chanceOfWinning;
            GameNumber = gameNumber;
        }

        public static PlayerMatchInformation Win(PlayerStatsBeforeMatch stats, EloCalculationDifference calculation)
        {
            return new PlayerMatchInformation(stats.PlayerId, calculation.EloGainedForWinner, stats.EloRating, stats.EloRating + calculation.EloGainedForWinner, calculation.WinChanceForWinner, ++stats.GameNumber);
        }

        public static PlayerMatchInformation Loss(PlayerStatsBeforeMatch stats, EloCalculationDifference calculation)
        {
            return new PlayerMatchInformation(stats.PlayerId, calculation.EloLossForLoser, stats.EloRating, stats.EloRating + calculation.EloLossForLoser, calculation.WinChanceForLoser, ++stats.GameNumber);
        }

        public Guid PlayerId { get; private set; }
        public decimal EloRatingGameDifference { get; private set; }
        public decimal TotalEloRatingBeforeGame { get; private set; }
        public decimal TotalEloRatingAfterGame { get; private set; }
        public decimal ChanceOfWinning { get; private set; }
        public int GameNumber { get; private set; }

        private PlayerMatchInformation() {}
    }
}