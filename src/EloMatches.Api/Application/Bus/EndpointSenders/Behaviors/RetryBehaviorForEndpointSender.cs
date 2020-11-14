using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EloMatches.Api.Application.Bus.EndpointSenders.Behaviors
{
    public class RetryBehaviorForEndpointSender<TCommand> : IEndpointSender<TCommand> where TCommand : notnull
    {
        private readonly IEndpointSender<TCommand> _next;
        private readonly ILogger<RetryBehaviorForEndpointSender<TCommand>> _logger;
        
        private int _retries;
        private const int MaxNumberOfRetries = 3;

        public RetryBehaviorForEndpointSender(IEndpointSender<TCommand> next, ILogger<RetryBehaviorForEndpointSender<TCommand>> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Send(TCommand command)
        {
            try
            {
                await _next.Send(command);
            }
            catch (Exception ex)
            {
                if (++_retries < MaxNumberOfRetries)
                {
                    await Send(command);
                    return;
                }
                
                _logger.LogError(ex, $"Error occurred while trying to send '{command.GetType().Name}' through bus");
            }
        }
    }
}