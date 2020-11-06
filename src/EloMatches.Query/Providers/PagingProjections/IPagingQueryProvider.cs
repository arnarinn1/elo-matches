using System;
using System.Linq.Expressions;
using EloMatches.Query.Paging;

namespace EloMatches.Query.Providers.PagingProjections
{
    public interface IPagingQueryProvider<TProjection> where TProjection : class
    {
        QueryRepresentation<TProjection> CreateQueries(Expression<Func<TProjection, bool>> evalExpression, IPagingQuery query);
    }
}