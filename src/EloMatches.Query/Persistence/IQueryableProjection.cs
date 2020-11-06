using System.Linq;

namespace EloMatches.Query.Persistence
{
    public interface IQueryableProjection
    {
        IQueryable<TProjection> Create<TProjection>() where TProjection : class;
    }
}