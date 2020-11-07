using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate.DomainEvents
{
    public class PlayerAddedToLeaderBoard : DomainEvent
    {
        public PlayerAddedToLeaderBoard(PlayerId playerId, decimal eloRating, int rank) : base(playerId, "PlayerLeaderBoard")
        {
            PlayerId = playerId;
            EloRating = eloRating;
            Rank = rank;
        }

        public PlayerId PlayerId { get; }
        public decimal EloRating { get; }
        public int Rank { get; }
    }
}