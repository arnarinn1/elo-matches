using System;
using System.Linq.Expressions;
using EloMatches.Query.AbstractHandlers;
using EloMatches.Query.Projections.DomainEventOccurrences;
using EloMatches.Query.Providers.PagingProjections;
using EloMatches.Shared.Extensions;

namespace EloMatches.Api.Features.DomainEventOccurrences.Queries.DomainEventsByPaging
{
    public class DomainEventsByPagingQueryHandler : BasePagingQueryHandler<DomainEventsByPagingQuery, DomainEventOccurrenceProjection>
    {
        public DomainEventsByPagingQueryHandler(IPagingQueryProvider<DomainEventOccurrenceProjection> pagingProvider) : base(pagingProvider) { }

        protected override Expression<Func<DomainEventOccurrenceProjection, bool>> CreateEvalExpression(DomainEventsByPagingQuery query)
        {
            Expression<Func<DomainEventOccurrenceProjection, bool>> where = x => true;

            if (!string.IsNullOrWhiteSpace(query.AggregateId))
                where = where.AndAlso(x => x.AggregateId == query.AggregateId);

            if (!string.IsNullOrWhiteSpace(query.AggregateType))
                where = where.AndAlso(x => x.AggregateType== query.AggregateType);

            return where;
        }
    }
}