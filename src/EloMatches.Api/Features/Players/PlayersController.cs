using System;
using System.Threading.Tasks;
using EloMatches.Api.Features.Players.Commands.Activations;
using EloMatches.Api.Features.Players.Commands.Activations.HttpPayloads;
using EloMatches.Api.Features.Players.Commands.ChangeEmail;
using EloMatches.Api.Features.Players.Commands.ChangeEmail.HttpPayloads;
using EloMatches.Api.Features.Players.Commands.ChangePlayerNames;
using EloMatches.Api.Features.Players.Commands.ChangePlayerNames.HttpPayloads;
using EloMatches.Api.Features.Players.Commands.CreatePlayer;
using EloMatches.Api.Features.Players.Commands.CreatePlayer.HttpPayloads;
using EloMatches.Api.Features.Players.Queries.PlayerById;
using EloMatches.Api.Features.Players.Queries.PlayersByPaging;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using EloMatches.Query.Paging;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Projections.Players;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EloMatches.Api.Features.Players
{
    [ApiController]
    [Route("api/players")]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IQueryDispatcher _queryDispatcher;

        public PlayersController(IMediator mediator, IQueryDispatcher queryDispatcher)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> PlayersByPaging(string? name = null, int pageSize = 5, int pageIndex = 0)
        {
            return Ok(await _queryDispatcher.Dispatch<PlayersByPagingQuery, PagingResult<PlayerProjection>>(new PlayersByPagingQuery(pageSize, pageIndex, nameof(PlayerProjection.DisplayName), OrderByDirection.Asc, name)));
        }

        [HttpGet]
        [Route("{playerId:guid}", Name = nameof(PlayerById))]
        public async Task<IActionResult> PlayerById(Guid playerId)
        {
            if (playerId == Guid.Empty)
                return BadRequest();

            return Ok(await _queryDispatcher.Dispatch<PlayerByIdQuery, PlayerProjection>(new PlayerByIdQuery(new PlayerId(playerId))));
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerRequestBody body)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new CreatePlayerCommand(new PlayerId(Guid.NewGuid()), new Name(body.UserName), new Name(body.DisplayName), new EmailAddress(body.Email));
            var result = await _mediator.Send(command);

            return CreatedAtRoute(nameof(PlayerById), new {playerId = result.PlayerId}, result);
        }

        [HttpPut]
        [Route("{playerId:guid}/names")]
        public async Task<IActionResult> ChangePlayerNames(Guid playerId, [FromBody] ChangePlayerNamesRequestBody body)
        {
            if (!ModelState.IsValid || playerId == Guid.Empty)
                return BadRequest();

            await _mediator.Send(new ChangePlayerNamesCommand(new PlayerId(playerId), new Name(body.UserName), new Name(body.DisplayName)));
            
            return NoContent();
        }

        [HttpPut]
        [Route("{playerId:guid}/emails")]
        public async Task<IActionResult> ChangePlayerEmail(Guid playerId, [FromBody] ChangePlayerEmailRequestBody body)
        {
            if (!ModelState.IsValid || playerId == Guid.Empty)
                return BadRequest();

            await _mediator.Send(new ChangePlayerEmailCommand(new PlayerId(playerId), new EmailAddress(body.Email)));

            return NoContent();
        }

        [HttpPut]
        [Route("{playerId:guid}/activations")]
        public async Task<IActionResult> SetPlayerActiveStatus(Guid playerId, [FromBody] SetPlayerActiveStatusRequestBody body)
        {
            if (!ModelState.IsValid || playerId == Guid.Empty)
                return BadRequest();

            await _mediator.Send(new SetPlayerActiveStatusCommand(new PlayerId(playerId), body.PlayerActiveStatus == PlayerActiveStatusKind.Reactivate));

            return NoContent();
        }
    }
}