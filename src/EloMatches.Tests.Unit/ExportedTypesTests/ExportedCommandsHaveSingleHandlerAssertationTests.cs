using System.Linq;
using System.Windows.Input;
using EloMatches.Api;
using EloMatches.Infrastructure.CommandPipeline.AbstractHandlers;
using EloMatches.Shared.Extensions;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.ExportedTypesTests
{
    [TestFixture, Category("Unit")]
    public class ExportedCommandsHaveHandlersAssertationTests
    {
        [Test]
        public void AllExportedCommandsHaveASingleCorrespondingHandler()
        {
            var commandTypes = ReflectionHelpers.GetAllTypesImplementingInterface(typeof(ICommand), typeof(Startup).Assembly);
            var commandHandlers = ReflectionHelpers.GetAllTypesImplementingOpenGenericType(typeof(ICommandHandler<,>), typeof(Startup).Assembly).ToArray();

            foreach (var type in commandTypes)
            {
                var requestInterface = type.GetInterfaces()
                    .First(x => typeof(ICommand).IsAssignableFrom(x) && x.GetGenericArguments().Length > 0);

                var resultType = requestInterface.GetGenericArguments().First();

                var handlerType = typeof(ICommandHandler<,>).MakeGenericType(type, resultType);

                var correspondingHandlers = commandHandlers.Where(x => handlerType.IsAssignableFrom(x)).ToArray();

                if (correspondingHandlers.Length == 0)
                    Assert.Fail($"Command has no CommandHandler = '{type.Name}'");

                if (correspondingHandlers.Length > 1)
                    Assert.Fail($"Command has more then one CommandHandler = '{type.Name}'");
            }

            Assert.Pass();
        }
    }
}