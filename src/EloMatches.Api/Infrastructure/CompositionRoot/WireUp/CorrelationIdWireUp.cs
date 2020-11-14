using EloMatches.Api.Infrastructure.CorrelationIds;
using SimpleInjector;

namespace EloMatches.Api.Infrastructure.CompositionRoot.WireUp
{
    public static class CorrelationIdWireUp
    {
        public static Container RegisterCorrelationIdServices(this Container container)
        {
            container.Register<ICorrelationIdAccessor, CorrelationIdAccessorFromHttpContext>(Lifestyle.Scoped);
            return container;
        }
    }
}