using EloMatches.Query.Projections.PlayerRankings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloMatches.Query.Persistence.EntityConfigurations
{
    public class PlayerRankingProjectionEntityConfiguration : IEntityTypeConfiguration<PlayerRankingProjection>
    {
        public void Configure(EntityTypeBuilder<PlayerRankingProjection> builder)
        {
            builder.ToView("vw_PlayerRanking", "elo");

            builder.Property(x => x.CurrentEloRating).HasPrecision(10, 6);
            builder.Property(x => x.AverageEloRating).HasPrecision(10, 6);
            builder.Property(x => x.LowestEloRating).HasPrecision(10, 6);
            builder.Property(x => x.HighestEloRating).HasPrecision(10, 6);
            builder.Property(x => x.EloRatingDifference).HasPrecision(10, 6);

            builder.Property(x => x.WinPercentage).HasPrecision(5, 4);
        }
    }
}