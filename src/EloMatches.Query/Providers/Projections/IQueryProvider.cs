using System;
using System.Linq;
using System.Linq.Expressions;

namespace EloMatches.Query.Providers.Projections
{
    public interface IQueryProvider<TProjection>
    {
        IQueryable<TProjection> CreateQuery(Expression<Func<TProjection, bool>> queryPredicate);
    }
}