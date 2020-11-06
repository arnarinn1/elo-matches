using System;
using System.Linq.Expressions;
using EloMatches.Query.AbstractHandlers;
using EloMatches.Query.Projections.PlayerRankings;
using EloMatches.Query.Providers.PagingProjections;

namespace EloMatches.Api.Features.PlayerRankings.Queries.PlayerRankingsByPaging
{
    public class PlayerRankingsByPagingQueryHandler : BasePagingQueryHandler<PlayerRankingsByPagingQuery, PlayerRankingProjection>
    {
        public PlayerRankingsByPagingQueryHandler(IPagingQueryProvider<PlayerRankingProjection> pagingProvider) : base(pagingProvider) { }

        protected override Expression<Func<PlayerRankingProjection, bool>> CreateEvalExpression(PlayerRankingsByPagingQuery query)
            => _ => true;
    }
}