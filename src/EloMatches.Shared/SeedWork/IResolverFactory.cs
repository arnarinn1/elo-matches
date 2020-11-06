using System;
using System.Collections.Generic;

namespace EloMatches.Shared.SeedWork
{
    public interface IResolverFactory
    {
        object ResolveInstance(Type type);
        IEnumerable<object> ResolveInstances(Type type);
    }
}