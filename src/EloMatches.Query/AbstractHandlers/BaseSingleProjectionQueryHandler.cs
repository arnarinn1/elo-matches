using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Providers.Projections;

namespace EloMatches.Query.AbstractHandlers
{
    public abstract class BaseSingleProjectionQueryHandler<TQuery, TProjection> : IQueryHandler<TQuery, TProjection> where TQuery : IQuery<TProjection>
    {
        private readonly IQueryProvider<TProjection>_provider ;

        protected BaseSingleProjectionQueryHandler(IQueryProvider<TProjection> provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        protected abstract Expression<Func<TProjection, bool>> CreateQueryPredicate(TQuery query);

        public Task<TProjection> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            return _provider.ExecuteSingleOrDefaultAsync(CreateQueryPredicate(query), cancellationToken);
        }
    }
}