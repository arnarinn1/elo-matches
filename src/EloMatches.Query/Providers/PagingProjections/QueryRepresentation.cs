using System.Linq;

namespace EloMatches.Query.Providers.PagingProjections
{
    public class QueryRepresentation<TProjection>
    {
        public QueryRepresentation(IQueryable<TProjection> whereQuery, IQueryable<TProjection> pagingQuery)
        {
            WhereQuery = whereQuery;
            PagingQuery = pagingQuery;
        }

        public IQueryable<TProjection> WhereQuery { get; }
        public IQueryable<TProjection> PagingQuery { get; }
    }
}