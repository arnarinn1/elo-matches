using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EloMatches.Shared.Extensions
{
    public class ReflectionHelpers
    {
        public static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(Type openGenericType, Assembly assembly)
        {
            return from x in assembly.GetTypes()
                from z in x.GetInterfaces()
                let y = x.BaseType
                where
                    (y != null && y.IsGenericType &&
                     openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition())) ||
                    (z.IsGenericType &&
                     openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition()))
                select x;
        }

        public static IEnumerable<Type> GetAllTypesImplementingInterface(Type interfaceType, Assembly assembly)
        {
            return assembly.ExportedTypes.Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToArray();
        }
    }
}