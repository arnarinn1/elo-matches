using EloMatches.Domain.AggregateModels.MatchesAggregate;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate;
using EloMatches.Domain.AggregateModels.PlayerRankingAggregate;
using EloMatches.Infrastructure.Persistence;
using EloMatches.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace EloMatches.Api.Infrastructure.CompositionRoot.WireUp
{
    public static class PersistenceWireUp
    {
        public static Container RegisterPersistence(this Container container, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];
            var enableSensitiveDataLogging = configuration.GetValue("EnableSensitiveDataLogging", false);

            container.Register<EloMatchesCommandContext>(() =>
            {
                var builder = new DbContextOptionsBuilder<EloMatchesCommandContext>()
                    .UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging(enableSensitiveDataLogging)
                    .UseLoggerFactory(container.GetInstance<ILoggerFactory>());

                return new EloMatchesCommandContext(builder.Options);
            }, Lifestyle.Scoped);

            container.Register<IPlayerRepository, PlayerRepository>(Lifestyle.Scoped);
            container.Register<IPlayerRankingRepository, PlayerRankingRepository>(Lifestyle.Scoped);
            container.Register<IMatchesAfterReferenceDateRepository, MatchesAfterReferenceDateRepository>(Lifestyle.Scoped);
            container.Register<IPlayerLeaderBoardRepository, PlayerLeaderBoardRepository>(Lifestyle.Scoped);

            return container;
        }
    }
}