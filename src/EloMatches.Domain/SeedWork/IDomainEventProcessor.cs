using System.Collections.Generic;
using System.Threading.Tasks;

namespace EloMatches.Domain.SeedWork
{
    public interface IDomainEventProcessor
    {
        Task Process(IReadOnlyCollection<IDomainEvent> domainEvents);
    }
}