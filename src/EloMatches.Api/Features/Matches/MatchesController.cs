using System;
using System.Threading.Tasks;
using EloMatches.Api.Features.Matches.Commands.RegisterMatch;
using EloMatches.Api.Features.Matches.Commands.RegisterMatch.HttpPayloads;
using EloMatches.Api.Features.Matches.Queries.MatchById;
using EloMatches.Api.Features.Matches.Queries.MatchesByPaging;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using EloMatches.Query.Paging;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Projections.Matches;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EloMatches.Api.Features.Matches
{
    [ApiController]
    [Route("api/matches")]
    public class MatchesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IQueryDispatcher _queryDispatcher;

        public MatchesController(IMediator mediator, IQueryDispatcher queryDispatcher)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> MatchesByPaging(DateTime matchFromDate, DateTime? matchToDate = null, Guid? playerId = null, int pageSize = 5, int pageIndex = 0)
        {
            return Ok(await _queryDispatcher.Dispatch<MatchesByPagingQuery, PagingResult<MatchProjection>>(new MatchesByPagingQuery(pageSize, pageIndex, nameof(MatchProjection.MatchDate), OrderByDirection.Asc, matchFromDate, matchToDate, playerId)));
        }

        [HttpGet]
        [Route("{matchId:int}", Name = nameof(MatchById))]
        public async Task<IActionResult> MatchById(int matchId)
        {
            return Ok(await _queryDispatcher.Dispatch<MatchByIdQuery, MatchProjection>(new MatchByIdQuery(matchId)));
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterMatch([FromBody] RegisterMatchRequestBody body)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new RegisterMatchCommand(new PlayerId(body.WinnerPlayerId), new PlayerId(body.LoserPlayerId), new MatchResult(body.WinnerScore, body.LoserScore), new MatchDate(body.MatchDate));
            var result = await _mediator.Send(command);

            return CreatedAtRoute(nameof(MatchById), new { matchId = result }, result);
        }
    }
}