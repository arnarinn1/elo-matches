using System;
using EloMatches.Domain.AggregateModels.PlayerAggregate;

namespace EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate
{
    public class PlayerOnLeaderBoard
    {
        public PlayerOnLeaderBoard(Guid playerId, decimal eloRating, int rank)
        {
            PlayerId = playerId;
            EloRating = eloRating;
            Rank = rank;
        }

        public Guid PlayerId { get; set; }
        public decimal EloRating { get; set; }
        public int Rank { get; set; }

        public PlayerId GetPlayerId() 
            => new PlayerId(PlayerId);
    }
}