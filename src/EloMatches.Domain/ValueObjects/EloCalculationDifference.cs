using System;

namespace EloMatches.Domain.ValueObjects
{
    public class EloCalculationDifference
    {
        private const int KFactor = 15;

        public decimal EloGainedForWinner { get; private set; }
        public decimal EloLossForLoser { get; private set; }
        public decimal WinChanceForWinner { get; private set; }
        public decimal WinChanceForLoser { get; private set; }

        public EloCalculationDifference(decimal eloRatingOfWinner, decimal eloRatingOfLoser)
        {
            var expectedScore = ExpectedScore((float)eloRatingOfWinner, (float)eloRatingOfLoser);

            double eloDifference = KFactor * (1 - expectedScore);

            EloGainedForWinner = (decimal) eloDifference;
            EloLossForLoser = -EloGainedForWinner;

            WinChanceForWinner = Math.Round((decimal)expectedScore, 4);
            WinChanceForLoser = 1.0m - WinChanceForWinner;
        }

        private EloCalculationDifference() {}

        private static float ExpectedScore(float playerRating, float opponentRating)
            => 1 / (1.0f + (float)Math.Pow(10.0f, (opponentRating - playerRating) / 400.0f));
    }
}