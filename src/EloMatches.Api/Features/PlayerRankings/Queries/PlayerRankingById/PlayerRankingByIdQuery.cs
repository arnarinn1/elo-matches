using System;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Projections.PlayerRankings;

namespace EloMatches.Api.Features.PlayerRankings.Queries.PlayerRankingById
{
    public class PlayerRankingByIdQuery : IQuery<PlayerRankingProjection>
    {
        public PlayerRankingByIdQuery(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}