using System;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Api.Application.Bus.EndpointSenders;
using EloMatches.Api.Application.Bus.EndpointSenders.QueryMetrics;
using EloMatches.Api.Infrastructure.CorrelationIds;
using EloMatches.Query.Pipeline;
using EloMatches.Shared.Extensions;

namespace EloMatches.Api.Application.QueryBehaviors
{
    public class SendQueryMetricsThroughBusBehavior<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        private readonly IQueryHandler<TQuery, TResponse> _next;
        private readonly IEndpointSender<LogQueryMetricsCommand> _endpointSender;
        private readonly ICorrelationIdAccessor _correlationIdAccessor;

        public SendQueryMetricsThroughBusBehavior(IQueryHandler<TQuery, TResponse> next, IEndpointSender<LogQueryMetricsCommand> endpointSender, ICorrelationIdAccessor correlationIdAccessor)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _endpointSender = endpointSender;
            _correlationIdAccessor = correlationIdAccessor ?? throw new ArgumentNullException(nameof(correlationIdAccessor));
        }

        public async Task<TResponse> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            var queryMetrics = new LogQueryMetricsCommand(_correlationIdAccessor.GetCorrelationId(), DateTime.Now, query.GetType().Name);

            TResponse response;

            try
            {
                response = await _next.ExecuteAsync(query, cancellationToken);
                queryMetrics.TimeFinished = DateTime.Now;

                await _endpointSender.Send(queryMetrics);
            }
            catch (Exception e)
            {
                queryMetrics.TimeFinished = DateTime.Now;
                queryMetrics.ExceptionMessage = e.GetInnermostExceptionMessage();

                await _endpointSender.Send(queryMetrics);

                throw;
            }
            
            return response;
        }
    }
}