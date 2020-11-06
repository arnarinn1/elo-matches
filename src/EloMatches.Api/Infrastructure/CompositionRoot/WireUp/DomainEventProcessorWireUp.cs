using EloMatches.Domain.SeedWork;
using SimpleInjector;

namespace EloMatches.Api.Infrastructure.CompositionRoot.WireUp
{
    public static class DomainEventProcessorWireUp
    {
        public static Container RegisterDomainEventProcessors(this Container container)
        {
            container.Collection.Register<IDomainEventProcessor>(typeof(Startup).Assembly);
            return container;
        }
    }
}