using EloMatches.Domain.AggregateModels.PlayerRankingAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloMatches.Infrastructure.Persistence.EntityConfigurations
{
    public class PlayerRankingEntityTypeConfiguration : IEntityTypeConfiguration<PlayerRanking>
    {
        public void Configure(EntityTypeBuilder<PlayerRanking> builder)
        {
            builder.ToTable("PlayerRanking");

            builder.HasKey(x => x.PlayerId);
            builder.Property(x => x.PlayerId).HasColumnName("Id");

            builder.OwnsOne(x => x.EloRatingStatistics, buildAction =>
            {
                buildAction.Property(x => x.CurrentEloRating).HasColumnName("CurrentEloRating").HasPrecision(10,6);
                buildAction.Property(x => x.AverageEloRating).HasColumnName("AverageEloRating").HasPrecision(10, 6);
                buildAction.Property(x => x.LowestEloRating).HasColumnName("LowestEloRating").HasPrecision(10, 6);
                buildAction.Property(x => x.HighestEloRating).HasColumnName("HighestEloRating").HasPrecision(10, 6);
                buildAction.Property(x => x.EloRatingDifference).HasColumnName("EloRatingDifference").HasPrecision(10, 6);
            });
            
            builder.OwnsOne(x => x.NumberOfMatchesStatistics, buildAction =>
            {
                buildAction.Property(x => x.PlayedGames).HasColumnName("PlayedGames");
                buildAction.Property(x => x.Wins).HasColumnName("Wins");
                buildAction.Property(x => x.Losses).HasColumnName("Losses");
                buildAction.Property(x => x.Differential).HasColumnName("WinDifferential");
                buildAction.Property(x => x.WinPercentage).HasColumnName("WinPercentage").HasPrecision(5, 4);
            });

            builder.OwnsOne(x => x.ScoreStatistics, buildAction =>
            {
                buildAction.Property(x => x.ScorePlus).HasColumnName("ScorePlus");
                buildAction.Property(x => x.ScoreMinus).HasColumnName("ScoreMinus");
                buildAction.Property(x => x.ScoreDifferential).HasColumnName("ScoreDifferential");
            });

            builder.OwnsOne(x => x.Streak, buildAction =>
            {
                buildAction.Property(x => x.StreakCount).HasColumnName("StreakCount");
                buildAction.Property(x => x.Type).HasColumnName("StreakType");
            });

            builder.Property(x => x.LastMatchDate).HasColumnName("LastMatchDate");

            builder.Ignore(b => b.DomainEvents);
        }
    }
}