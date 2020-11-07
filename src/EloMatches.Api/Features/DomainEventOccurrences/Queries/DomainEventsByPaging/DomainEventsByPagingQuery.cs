using EloMatches.Query.Paging;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Projections.DomainEventOccurrences;

namespace EloMatches.Api.Features.DomainEventOccurrences.Queries.DomainEventsByPaging
{
    public class DomainEventsByPagingQuery : BasePagingQuery, IQuery<PagingResult<DomainEventOccurrenceProjection>>
    {
        public DomainEventsByPagingQuery(int pageSize, int pageIndex, string orderByColumn, OrderByDirection orderByDirection, string? aggregateId, string? aggregateType) : base(pageSize, pageIndex, orderByColumn, orderByDirection)
        {
            AggregateId = aggregateId;
            AggregateType = aggregateType;
        }

        public string? AggregateId { get; }
        public string? AggregateType { get; }
    }
}