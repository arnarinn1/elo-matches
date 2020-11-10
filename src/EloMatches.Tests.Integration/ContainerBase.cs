using System;
using System.IO;
using System.Threading.Tasks;
using EloMatches.Api.Infrastructure.CompositionRoot.WireUp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace EloMatches.Tests.Integration
{
    public class ContainerBase
    {
        private readonly Container _container;

        public ContainerBase()
        {
            _container = new Container
            {
                Options = { DefaultLifestyle = Lifestyle.Scoped, DefaultScopedLifestyle = new AsyncScopedLifestyle() }
            };

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettingsTest.json", optional: false, reloadOnChange: false)
                .Build();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            
            _container.Register<ILoggerFactory>(() => new Serilog.Extensions.Logging.SerilogLoggerFactory(logger), Lifestyle.Singleton);
            _container.Register(typeof(ILogger<>), typeof(Logger<>), Lifestyle.Singleton);

            _container
                .RegisterMediatorPipeline()
                .RegisterPersistence(configuration)
                .RegisterQueryPipeline(configuration)
                .RegisterDomainEventProcessors()
                .RegisterIntegrationEventPipeline();
        }

        protected TInstance GetInstance<TInstance>() where TInstance : class
        {
            return _container.GetInstance<TInstance>();
        }

        protected async Task ExecuteTest(Func<Container, Task> test)
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                await test(_container);
            }
        }

        protected async Task ExecuteTestWithInstance<TInstance>(Func<TInstance, Task> test) where TInstance : class
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                await test(_container.GetInstance<TInstance>());
            }
        }

        protected async Task ExecuteTestWithInstance<TInstance1, TInstance2>(Func<TInstance1, TInstance2, Task> test) where TInstance1 : class where TInstance2 : class
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                await test(_container.GetInstance<TInstance1>(), _container.GetInstance<TInstance2>());
            }
        }
    }
}