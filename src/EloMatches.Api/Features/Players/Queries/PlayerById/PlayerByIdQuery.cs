using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Projections.Players;

namespace EloMatches.Api.Features.Players.Queries.PlayerById
{
    public class PlayerByIdQuery : IQuery<PlayerProjection>
    {
        public PlayerByIdQuery(PlayerId playerId)
        {
            PlayerId = playerId;
        }

        public PlayerId PlayerId { get; }
    }
}