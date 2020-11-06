using EloMatches.Query.Paging;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Projections.PlayerRankings;

namespace EloMatches.Api.Features.PlayerRankings.Queries.PlayerRankingsByPaging
{
    public class PlayerRankingsByPagingQuery : BasePagingQuery, IQuery<PagingResult<PlayerRankingProjection>>
    {
        public PlayerRankingsByPagingQuery(int pageSize, int pageIndex, string orderByColumn, OrderByDirection orderByDirection) : base(pageSize, pageIndex, orderByColumn, orderByDirection) { }
    }
}