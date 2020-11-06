using System;
using System.Linq.Expressions;
using EloMatches.Query.AbstractHandlers;
using EloMatches.Query.Projections.Players;
using EloMatches.Query.Providers.PagingProjections;
using EloMatches.Shared.Extensions;

namespace EloMatches.Api.Features.Players.Queries.PlayersByPaging
{
    public class PlayersByPagingQueryHandler : BasePagingQueryHandler<PlayersByPagingQuery, PlayerProjection>
    {
        public PlayersByPagingQueryHandler(IPagingQueryProvider<PlayerProjection> pagingProvider) 
            : base(pagingProvider) { }

        protected override Expression<Func<PlayerProjection, bool>> CreateEvalExpression(PlayersByPagingQuery query)
        {
            Expression<Func<PlayerProjection, bool>> where = _ => true;

            if (!query.IncludeDeactivatedPlayers)
                where = x => x.IsActive;

            return string.IsNullOrWhiteSpace(query.UserName) 
                ? where 
                : where.AndAlso(x => query.UserName.Contains(x.UserName));
        }
    }
}