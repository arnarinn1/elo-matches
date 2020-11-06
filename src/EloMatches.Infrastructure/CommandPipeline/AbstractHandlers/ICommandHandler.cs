using MediatR;

namespace EloMatches.Infrastructure.CommandPipeline.AbstractHandlers
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> 
        where TCommand : ICommand<TResponse> { }
}