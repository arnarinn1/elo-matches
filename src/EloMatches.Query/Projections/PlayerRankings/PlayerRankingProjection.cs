using System;

namespace EloMatches.Query.Projections.PlayerRankings
{
    public class PlayerRankingProjection
    {
        public Guid Id { get; set; }
        public int SequenceId { get; set; }

        public string UserName { get; set; }
        public string DisplayName { get; set; }

        public decimal CurrentEloRating { get; set; }
        public decimal AverageEloRating { get; set; }
        public decimal LowestEloRating { get; set; }
        public decimal HighestEloRating { get; set; }
        public decimal EloRatingDifference { get; set; }

        public int PlayedGames { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int WinDifferential { get; set; }
        public decimal WinPercentage { get; set; }

        public int ScorePlus { get; set; }
        public int ScoreMinus { get; set; }
        public int ScoreDifferential { get; set; }

        public int StreakCount { get; set; }
        public bool OnWinningStreak{ get; set; }

        public DateTime? LastMatchDate { get; set; }
    }
}