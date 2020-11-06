using System.Linq;
using EloMatches.Api;
using EloMatches.Query.Pipeline;
using EloMatches.Shared.Extensions;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.ExportedTypesTests
{
    [TestFixture, Category("Unit")]
    public class ExportedQueriesHaveSingleHandlerAssertationTests
    {
        [Test]
        public void AllExportedCommandsHaveASingleCorrespondingHandler()
        {
            var queryTypes = ReflectionHelpers.GetAllTypesImplementingInterface(typeof(IQuery), typeof(Startup).Assembly);
            var queryHandlerTypes = ReflectionHelpers.GetAllTypesImplementingOpenGenericType(typeof(IQueryHandler<,>), typeof(Startup).Assembly).ToArray();

            foreach (var queryType in queryTypes)
            {
                var requestInterface = queryType.GetInterfaces()
                    .First(x => typeof(IQuery).IsAssignableFrom(x) && x.GetGenericArguments().Length > 0);

                var resultType = requestInterface.GetGenericArguments().First();

                var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, resultType);

                var correspondingHandlers = queryHandlerTypes.Where(x => handlerType.IsAssignableFrom(x)).ToArray();

                if (correspondingHandlers.Length == 0)
                    Assert.Fail($"Query has no QueryHandler = '{queryType.Name}'");

                if (correspondingHandlers.Length > 1)
                    Assert.Fail($"Query has more then one QueryHandler = '{queryType.Name}'");
            }

            Assert.Pass();
        }
    }
}