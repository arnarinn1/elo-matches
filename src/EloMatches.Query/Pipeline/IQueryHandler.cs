using System.Threading;
using System.Threading.Tasks;

namespace EloMatches.Query.Pipeline
{
    public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        Task<TResponse> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}