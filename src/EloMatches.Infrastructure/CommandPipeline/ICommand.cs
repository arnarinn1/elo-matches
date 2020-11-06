using MediatR;

namespace EloMatches.Infrastructure.CommandPipeline
{
    public interface ICommand {}
    public interface ICommand<out TResponse> : ICommand, IRequest<TResponse> { }
}