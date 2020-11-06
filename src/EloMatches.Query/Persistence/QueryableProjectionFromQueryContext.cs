using System;
using System.Linq;

namespace EloMatches.Query.Persistence
{
    public class QueryableProjectionFromQueryContext : IQueryableProjection
    {
        private readonly EloMatchesQueryContext _context;

        public QueryableProjectionFromQueryContext(EloMatchesQueryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<TProjection> Create<TProjection>() where TProjection : class
        {
            return _context.Set<TProjection>();
        }
    }
}