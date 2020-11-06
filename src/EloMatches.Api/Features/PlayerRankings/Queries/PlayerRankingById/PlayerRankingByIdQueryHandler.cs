using System;
using System.Linq.Expressions;
using EloMatches.Query.AbstractHandlers;
using EloMatches.Query.Projections.PlayerRankings;
using EloMatches.Query.Providers.Projections;

namespace EloMatches.Api.Features.PlayerRankings.Queries.PlayerRankingById
{
    public class PlayerRankingByIdQueryHandler : BaseSingleProjectionQueryHandler<PlayerRankingByIdQuery, PlayerRankingProjection>
    {
        public PlayerRankingByIdQueryHandler(IQueryProvider<PlayerRankingProjection> provider) : base(provider) { }

        protected override Expression<Func<PlayerRankingProjection, bool>> CreateQueryPredicate(PlayerRankingByIdQuery query)
            => x => x.Id == query.PlayerId;
    }
}