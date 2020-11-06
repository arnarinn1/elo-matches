using EloMatches.Query.Paging;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Projections.Players;

namespace EloMatches.Api.Features.Players.Queries.PlayersByPaging
{
    public class PlayersByPagingQuery : BasePagingQuery, IQuery<PagingResult<PlayerProjection>>
    {
        public PlayersByPagingQuery(int pageSize, int pageIndex, string orderByColumn, OrderByDirection orderByDirection, string? userName = null, bool includeDeactivatedPlayers = false)
            : base(pageSize, pageIndex, orderByColumn, orderByDirection)
        {
            UserName = userName;
            IncludeDeactivatedPlayers = includeDeactivatedPlayers;
        }

        public string? UserName { get; }
        public bool IncludeDeactivatedPlayers { get; }
    }
}