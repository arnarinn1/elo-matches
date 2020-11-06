using System;
using System.Linq.Expressions;
using EloMatches.Query.AbstractHandlers;
using EloMatches.Query.Projections.Matches;
using EloMatches.Query.Providers.Projections;

namespace EloMatches.Api.Features.Matches.Queries.MatchById
{
    public class MatchByIdQueryHandler : BaseSingleProjectionQueryHandler<MatchByIdQuery, MatchProjection>
    {
        public MatchByIdQueryHandler(IQueryProvider<MatchProjection> provider) : base(provider) { }

        protected override Expression<Func<MatchProjection, bool>> CreateQueryPredicate(MatchByIdQuery query)
            => x => x.Id == query.MatchId;
    }
}