using System;
using System.Linq.Expressions;
using EloMatches.Query.AbstractHandlers;
using EloMatches.Query.Projections.Matches;
using EloMatches.Query.Providers.PagingProjections;
using EloMatches.Shared.Extensions;

namespace EloMatches.Api.Features.Matches.Queries.MatchesByPaging
{
    public class MatchesByPagingQueryHandler : BasePagingQueryHandler<MatchesByPagingQuery, MatchProjection>
    {
        public MatchesByPagingQueryHandler(IPagingQueryProvider<MatchProjection> pagingProvider) : base(pagingProvider) { }

        protected override Expression<Func<MatchProjection, bool>> CreateEvalExpression(MatchesByPagingQuery query)
        {
            Expression<Func<MatchProjection, bool>> where = x => x.MatchDate >= query.MatchFromDate;

            if (query.MatchToDate is not null)
                where = where.AndAlso(x => x.MatchDate <= query.MatchToDate);

            if (query.PlayerId is not null)
                where = where.AndAlso(x => x.PlayerIdOfWinner == query.PlayerId || x.PlayerIdOfLoser == query.PlayerId);

            return where;
        }
    }
}