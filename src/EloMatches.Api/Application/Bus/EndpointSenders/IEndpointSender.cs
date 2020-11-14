using System.Threading.Tasks;

namespace EloMatches.Api.Application.Bus.EndpointSenders
{
    public interface IEndpointSender<in TCommand>
    {
        Task Send(TCommand command);
    }
}