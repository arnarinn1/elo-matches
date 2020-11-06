using System;
using System.Threading.Tasks;
using EloMatches.Api.Features.PlayerRankings.Queries.PlayerRankingById;
using EloMatches.Api.Features.PlayerRankings.Queries.PlayerRankingsByPaging;
using EloMatches.Query.Paging;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Projections.PlayerRankings;
using Microsoft.AspNetCore.Mvc;

namespace EloMatches.Api.Features.PlayerRankings
{
    [ApiController]
    [Route("api/player-rankings")]
    public class PlayerRankingsController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public PlayerRankingsController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> PlayerRankingsByPaging(int pageSize = 5, int pageIndex = 0)
        {
            return Ok(await _queryDispatcher.Dispatch<PlayerRankingsByPagingQuery, PagingResult<PlayerRankingProjection>>(new PlayerRankingsByPagingQuery(pageSize, pageIndex, nameof(PlayerRankingProjection.CurrentEloRating), OrderByDirection.Desc)));
        }

        [HttpGet]
        [Route("{playerId:guid}", Name = nameof(PlayerRankingById))]
        public async Task<IActionResult> PlayerRankingById(Guid playerId)
        {
            if (playerId == Guid.Empty)
                return BadRequest();

            return Ok(await _queryDispatcher.Dispatch<PlayerRankingByIdQuery, PlayerRankingProjection>(new PlayerRankingByIdQuery(playerId)));
        }
    }
}