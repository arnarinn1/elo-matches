using System;
using System.Linq;
using System.Linq.Expressions;
using EloMatches.Query.Extensions;
using EloMatches.Query.Paging;
using EloMatches.Query.Persistence;

namespace EloMatches.Query.Providers.PagingProjections
{
    public class PagingQueryProvider<TProjection> : IPagingQueryProvider<TProjection> where TProjection : class
    {
        private readonly IQueryableProjection _queryableProjection;

        public PagingQueryProvider(IQueryableProjection queryableProjection)
        {
            _queryableProjection = queryableProjection ?? throw new ArgumentNullException(nameof(queryableProjection));
        }

        public QueryRepresentation<TProjection> CreateQueries(Expression<Func<TProjection, bool>> evalExpression, IPagingQuery query)
        {
            ValidateQueryParameters(query);

            var whereQuery = _queryableProjection.Create<TProjection>().Where(evalExpression);

            var orderByQuery = query.OrderByDirection == OrderByDirection.Asc
                ? whereQuery.OrderBy(query.OrderByColumn)
                : whereQuery.OrderByDescending(query.OrderByColumn);

            var pagingQuery = orderByQuery.Skip(query.PageSize * query.PageIndex).Take(query.PageSize);

            return new QueryRepresentation<TProjection>(whereQuery, pagingQuery);
        }

        private static void ValidateQueryParameters(IPagingQuery query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (string.IsNullOrWhiteSpace(query.OrderByColumn))
                throw new ArgumentException("Value can't be null or whitespace", nameof(query.OrderByColumn));

            if (query.PageSize < 0)
                throw new ArgumentOutOfRangeException(nameof(query.PageSize));

            if (query.PageIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(query.PageIndex));
        }
    }
}