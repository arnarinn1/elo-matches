using System;
using System.Collections.Generic;
using System.Linq;
using EloMatches.Shared.SeedWork;
using SimpleInjector;

namespace EloMatches.Api.Infrastructure.CompositionRoot.Implementations
{
    public class ResolverFactory : IResolverFactory
    {
        private readonly Container _container;
        public ResolverFactory(Container container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }
        public object ResolveInstance(Type type) => _container.GetInstance(type);
        public IEnumerable<object> ResolveInstances(Type type) => _container.GetAllInstances(type).ToArray();
    }
}