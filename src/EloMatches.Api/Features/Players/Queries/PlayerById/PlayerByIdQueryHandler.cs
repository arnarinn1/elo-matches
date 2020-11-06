using System;
using System.Linq.Expressions;
using EloMatches.Query.AbstractHandlers;
using EloMatches.Query.Projections.Players;
using EloMatches.Query.Providers.Projections;

namespace EloMatches.Api.Features.Players.Queries.PlayerById
{
    public class PlayerByIdQueryHandler : BaseSingleProjectionQueryHandler<PlayerByIdQuery, PlayerProjection>
    {
        public PlayerByIdQueryHandler(IQueryProvider<PlayerProjection> provider) : base(provider) { }

        protected override Expression<Func<PlayerProjection, bool>> CreateQueryPredicate(PlayerByIdQuery query) 
            => x => x.PlayerId == query.PlayerId.Id;
    }
}