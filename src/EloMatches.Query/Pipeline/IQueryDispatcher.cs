using System.Threading.Tasks;

namespace EloMatches.Query.Pipeline
{
    public interface IQueryDispatcher
    {
        Task<TResponse> Dispatch<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
    }
}