using System;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Api.Application.Bus.EndpointSenders;
using EloMatches.Api.Application.Bus.EndpointSenders.QueryMetrics;
using EloMatches.Query.Pipeline;

namespace EloMatches.Api.Application.QueryBehaviors
{
    //todo -> CorrelationId
    public class SendQueryMetricsThroughBusBehavior<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        private readonly IQueryHandler<TQuery, TResponse> _next;
        private readonly IEndpointSender<LogQueryMetricsCommand> _endpointSender;

        public SendQueryMetricsThroughBusBehavior(IQueryHandler<TQuery, TResponse> next, IEndpointSender<LogQueryMetricsCommand> endpointSender)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _endpointSender = endpointSender;
        }

        public async Task<TResponse> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            var queryMetrics = new LogQueryMetricsCommand(Guid.NewGuid(), DateTime.Now, query.GetType().Name);

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
                queryMetrics.ExceptionMessage = e.Message;

                await _endpointSender.Send(queryMetrics);

                throw;
            }
            
            return response;
        }
    }
}