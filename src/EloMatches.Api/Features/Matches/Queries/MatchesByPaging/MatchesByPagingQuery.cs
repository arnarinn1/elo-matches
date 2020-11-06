using System;
using EloMatches.Query.Paging;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Projections.Matches;

namespace EloMatches.Api.Features.Matches.Queries.MatchesByPaging
{
    public class MatchesByPagingQuery : BasePagingQuery, IQuery<PagingResult<MatchProjection>>
    {
        public MatchesByPagingQuery(int pageSize, int pageIndex, string orderByColumn, OrderByDirection orderByDirection, DateTime matchFromDate, DateTime? matchToDate = null, Guid? playerId = null) : base(pageSize, pageIndex, orderByColumn, orderByDirection)
        {
            MatchFromDate = matchFromDate;
            MatchToDate = matchToDate;
            PlayerId = playerId;
        }

        public DateTime MatchFromDate { get; }
        public DateTime? MatchToDate { get; }
        public Guid? PlayerId { get; }
    }
}