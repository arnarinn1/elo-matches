using System;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Api.Application.Bus.EndpointSenders;
using EloMatches.Api.Application.Bus.EndpointSenders.CommandMetrics;
using EloMatches.Api.Infrastructure.CorrelationIds;
using EloMatches.Shared.Extensions;
using MediatR;

namespace EloMatches.Api.Application.Behaviors
{
    public class SendCommandMetricsThroughBusBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEndpointSender<LogCommandMetricsCommand> _endpointSender;
        private readonly ICorrelationIdAccessor _correlationIdAccessor;

        public SendCommandMetricsThroughBusBehavior(IEndpointSender<LogCommandMetricsCommand> endpointSender, ICorrelationIdAccessor correlationIdAccessor)
        {
            _endpointSender = endpointSender ?? throw new ArgumentNullException(nameof(endpointSender));
            _correlationIdAccessor = correlationIdAccessor ?? throw new ArgumentNullException(nameof(correlationIdAccessor));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var commandMetrics = new LogCommandMetricsCommand(_correlationIdAccessor.GetCorrelationId(), DateTime.Now, request.GetType().Name);

            TResponse response;

            try
            {
                response = await next();
                commandMetrics.TimeFinished = DateTime.Now;

                await _endpointSender.Send(commandMetrics);
            }
            catch (Exception e)
            {
                commandMetrics.TimeFinished = DateTime.Now;
                commandMetrics.ExceptionMessage = e.GetInnermostExceptionMessage();

                await _endpointSender.Send(commandMetrics);

                throw;
            }

            return response;
        }
    }
}