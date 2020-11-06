using System.Collections.Generic;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.MatchesAggregate
{
    public class PlayerStatsBeforeMatch : ValueObject
    {
        public PlayerStatsBeforeMatch(PlayerId playerId, decimal eloRating, int gameNumber)
        {
            PlayerId = playerId;
            EloRating = eloRating;
            GameNumber = gameNumber;
        }

        public PlayerId PlayerId { get; set; }
        public decimal EloRating { get; set; }
        public int GameNumber { get; set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PlayerId;
        }
    }
}