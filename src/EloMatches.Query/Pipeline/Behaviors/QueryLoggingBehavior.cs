using System;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace EloMatches.Query.Pipeline.Behaviors
{
    public class QueryLoggingBehavior<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        private readonly IQueryHandler<TQuery, TResponse> _next;
        private readonly ILogger<QueryLoggingBehavior<TQuery, TResponse>> _logger;

        public QueryLoggingBehavior(IQueryHandler<TQuery, TResponse> next, ILogger<QueryLoggingBehavior<TQuery, TResponse>> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("----- Executing query {QueryName} ({@Query})", query.GetGenericTypeName(), query);

            var result = await _next.ExecuteAsync(query, cancellationToken);

            _logger.LogDebug("----- Executed query {QueryName}", query.GetGenericTypeName());

            return result;
        }
    }
}