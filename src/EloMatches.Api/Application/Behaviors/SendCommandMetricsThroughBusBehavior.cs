using System;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Api.Application.Bus.EndpointSenders;
using EloMatches.Api.Application.Bus.EndpointSenders.CommandMetrics;
using MediatR;

namespace EloMatches.Api.Application.Behaviors
{
    public class SendCommandMetricsThroughBusBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEndpointSender<LogCommandMetricsCommand> _endpointSender;

        public SendCommandMetricsThroughBusBehavior(IEndpointSender<LogCommandMetricsCommand> endpointSender)
        {
            _endpointSender = endpointSender ?? throw new ArgumentNullException(nameof(endpointSender));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var commandMetrics = new LogCommandMetricsCommand(Guid.NewGuid(), DateTime.Now, request.GetType().Name);

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
                commandMetrics.ExceptionMessage = e.Message;

                await _endpointSender.Send(commandMetrics);

                throw;
            }

            return response;
        }
    }
}