using System;

namespace EloMatches.Domain.AggregateModels.MatchesAggregate
{
    public class PlayerEloRatingStatus
    {
        public PlayerEloRatingStatus(Guid playerId, decimal currentEloRating)
        {
            PlayerId = playerId;
            CurrentEloRating = currentEloRating;
        }

        public Guid PlayerId { get; private set; }
        public decimal CurrentEloRating { get; private set; }

        private PlayerEloRatingStatus() {}

        public void AddWin(decimal eloRatingDifference)
        {
            CurrentEloRating += eloRatingDifference;
        }

        public void AddLoss(decimal eloRatingDifference)
        {
            CurrentEloRating -= Math.Abs(eloRatingDifference);
        }
    }
}