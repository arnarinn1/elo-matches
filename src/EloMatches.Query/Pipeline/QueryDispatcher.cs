using System;
using System.Linq;
using System.Threading.Tasks;
using EloMatches.Shared.SeedWork;

namespace EloMatches.Query.Pipeline
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IResolverFactory _resolverFactory;

        public QueryDispatcher(IResolverFactory resolverFactory)
        {
            _resolverFactory = resolverFactory ?? throw new ArgumentNullException(nameof(resolverFactory));
        }

        public Task<TResponse> Dispatch<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            var queryType = query.GetType();

            var resultType = queryType.GetInterfaces().First(x => typeof(IQuery).IsAssignableFrom(x)).GetGenericArguments().First();

            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, resultType);

            var queryHandler = (IQueryHandler<TQuery, TResponse>) _resolverFactory.ResolveInstance(handlerType);

            return queryHandler.ExecuteAsync(query);
        }
    }
}