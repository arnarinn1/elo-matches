using System.Linq;
using System.Threading.Tasks;
using EloMatches.Domain.SeedWork;
using EloMatches.Infrastructure.Persistence;
using MediatR;

namespace EloMatches.Infrastructure.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, EloMatchesCommandContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Aggregate>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToArray();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}