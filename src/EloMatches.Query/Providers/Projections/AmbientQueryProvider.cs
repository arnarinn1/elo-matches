using System;
using System.Linq;
using System.Linq.Expressions;
using EloMatches.Query.Persistence;

namespace EloMatches.Query.Providers.Projections
{
    public class AmbientQueryProvider<TProjection> : IQueryProvider<TProjection> where TProjection : class
    {
        private readonly IQueryableProjection _queryableProjection;

        public AmbientQueryProvider(IQueryableProjection queryableProjection)
        {
            _queryableProjection = queryableProjection;
        }

        public IQueryable<TProjection> CreateQuery(Expression<Func<TProjection,bool>> queryPredicate)
        {
            return _queryableProjection.Create<TProjection>().Where(queryPredicate);
        }
    }
}