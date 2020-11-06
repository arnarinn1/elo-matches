using System;

namespace EloMatches.Query.Projections.Matches
{
    public class MatchProjection
    {
        public int Id { get; set; }

        public Guid PlayerIdOfWinner { get; set; }
        public string UserNameOfWinner { get; set; }
        public string DisplayNameOfWinner { get; set; }

        public Guid PlayerIdOfLoser { get; set; }
        public string UserNameOfLoser { get; set; }
        public string DisplayNameOfLoser { get; set; }

        public int WinnerScore { get; set; }
        public int LoserScore { get; set; }

        public int WinnerGameNumber { get; set; }
        public int LoserGameNumber { get; set; }

        public decimal EloRatingGainedForWinner { get; set; }
        public decimal TotalEloRatingBeforeGameForWinner { get; set; }
        public decimal TotalEloRatingAfterGameForWinner { get; set; }
        public decimal WinChanceBasedOnEloRatingForWinner { get; set; }

        public decimal EloRatingLostForLoser { get; set; }
        public decimal TotalEloRatingBeforeGameForLoser { get; set; }
        public decimal TotalEloRatingAfterGameForLoser { get; set; }
        public decimal WinChanceBasedOnEloRatingForLoser { get; set; }

        public DateTime MatchDate { get; set; }
        public DateTime EntryDate { get; set; }
    }
}