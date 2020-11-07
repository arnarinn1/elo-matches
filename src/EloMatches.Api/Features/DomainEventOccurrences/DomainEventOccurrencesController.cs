using System;
using System.Threading.Tasks;
using EloMatches.Api.Features.DomainEventOccurrences.Queries.DomainEventsByPaging;
using EloMatches.Query.Paging;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Projections.DomainEventOccurrences;
using Microsoft.AspNetCore.Mvc;

namespace EloMatches.Api.Features.DomainEventOccurrences
{
    [ApiController]
    [Route("api/domain-event-occurrences")]
    public class DomainEventOccurrencesController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public DomainEventOccurrencesController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> PlayersByPaging(string? aggregateId = null, string? aggregateType = null, int pageSize = 5, int pageIndex = 0)
        {
            return Ok(await _queryDispatcher.Dispatch<DomainEventsByPagingQuery, PagingResult<DomainEventOccurrenceProjection>>(new DomainEventsByPagingQuery(pageSize, pageIndex, nameof(DomainEventOccurrenceProjection.OccurrenceDate), OrderByDirection.Desc, aggregateId, aggregateType)));
        }
    }
}