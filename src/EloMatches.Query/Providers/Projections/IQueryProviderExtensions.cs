using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EloMatches.Query.Providers.Projections
{
    // ReSharper disable once InconsistentNaming
    public static class IQueryProviderExtensions
    {
        public static Task<TProjection[]> ExecuteArrayAsync<TProjection>(this IQueryProvider<TProjection> self, Expression<Func<TProjection, bool>> queryPredicate, CancellationToken cancellationToken = default)
        {
            return self.CreateQuery(queryPredicate).ToArrayAsync(cancellationToken);
        }

        public static Task<TProjection> ExecuteSingleOrDefaultAsync<TProjection>(this IQueryProvider<TProjection> self, Expression<Func<TProjection, bool>> queryPredicate, CancellationToken cancellationToken = default)
        {
            return self.CreateQuery(queryPredicate).SingleOrDefaultAsync(cancellationToken);
        }
    }
}