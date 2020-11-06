using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Query.Paging;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Providers.PagingProjections;
using Microsoft.EntityFrameworkCore;

namespace EloMatches.Query.AbstractHandlers
{
    public abstract class BasePagingQueryHandler<TQuery, TProjection> : IQueryHandler<TQuery, PagingResult<TProjection>> 
        where TQuery : IPagingQuery, IQuery<PagingResult<TProjection>>
        where TProjection : class
    {
        private readonly IPagingQueryProvider<TProjection> _pagingProvider;

        protected BasePagingQueryHandler(IPagingQueryProvider<TProjection> pagingProvider)
        {
            _pagingProvider = pagingProvider ?? throw new ArgumentNullException(nameof(pagingProvider));
        }

        protected abstract Expression<Func<TProjection, bool>> CreateEvalExpression(TQuery query);

        public async Task<PagingResult<TProjection>> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            var queries = _pagingProvider.CreateQueries(CreateEvalExpression(query), query);

            var projections = await queries.PagingQuery.ToArrayAsync(cancellationToken);

            var totalCount = query.PageIndex == 0 && query.PageSize > projections.Length
                ? projections.Length
                : await queries.WhereQuery.CountAsync(cancellationToken);

            return new PagingResult<TProjection>(totalCount, query, projections);
        }
    }
}