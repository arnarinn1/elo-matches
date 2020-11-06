using EloMatches.Query.Persistence;
using EloMatches.Query.Pipeline;
using EloMatches.Query.Pipeline.Behaviors;
using EloMatches.Query.Providers.PagingProjections;
using EloMatches.Query.Providers.Projections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace EloMatches.Api.Infrastructure.CompositionRoot.WireUp
{
    public static class QueryWireUp
    {
        public static Container RegisterQueryPipeline(this Container container, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];
            var enableSensitiveDataLogging = configuration.GetValue("EnableSensitiveDataLogging", false);

            container.Register<EloMatchesQueryContext>(() =>
            {
                var builder = new DbContextOptionsBuilder<EloMatchesQueryContext>()
                    .UseSqlServer(connectionString)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .EnableSensitiveDataLogging(enableSensitiveDataLogging)
                    .UseLoggerFactory(container.GetInstance<ILoggerFactory>());

                return new EloMatchesQueryContext(builder.Options);
            }, Lifestyle.Scoped);

            container.Register(typeof(IQueryHandler<,>), typeof(QueryWireUp).Assembly);
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(QueryLoggingBehavior<,>));

            container.RegisterSingleton<IQueryDispatcher, QueryDispatcher>();

            container.Register(typeof(IQueryProvider<>), typeof(AmbientQueryProvider<>));
            container.Register(typeof(IPagingQueryProvider<>), typeof(PagingQueryProvider<>));

            container.Register<IQueryableProjection, QueryableProjectionFromQueryContext>(Lifestyle.Scoped);

            return container;
        }
    }
}