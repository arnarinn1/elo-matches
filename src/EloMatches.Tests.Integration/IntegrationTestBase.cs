﻿using System;
using System.IO;
using System.Threading.Tasks;
using EloMatches.Api.Infrastructure.CompositionRoot.WireUp;
using EloMatches.Infrastructure.CommandPipeline;
using EloMatches.Query.Pipeline;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Respawn;
using Serilog;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace EloMatches.Tests.Integration
{
    internal abstract class IntegrationTestBase
    {
        private readonly Container _container;

        private readonly string _connectionString;

        protected IntegrationTestBase()
        {
            _container = new Container
            {
                Options = { DefaultLifestyle = Lifestyle.Scoped, DefaultScopedLifestyle = new AsyncScopedLifestyle() }
            };

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettingsTest.json", optional: false, reloadOnChange: false)
                .Build();

            _connectionString = configuration["ConnectionString"];

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

        private static readonly Checkpoint Checkpoint = new Checkpoint {SchemasToInclude = new[] {"elo"}, DbAdapter = DbAdapter.SqlServer};

        [SetUp]
        public virtual void SetUpBeforeEachTest()
        {

        }

        [TearDown]
        public virtual async Task RunAfterEachTest()
        {
            await Checkpoint.Reset(_connectionString);
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

        protected async Task Command(ICommand command)
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var mediator = _container.GetInstance<IMediator>();
                await mediator.Send(command);
            }
        }

        protected async Task<TResult> Query<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            await using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var queryDispatcher = _container.GetInstance<IQueryDispatcher>();
                return await queryDispatcher.Dispatch<TQuery, TResult>(query);
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