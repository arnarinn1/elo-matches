using EloMatches.Query.Projections.Matches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloMatches.Query.Persistence.EntityConfigurations
{
    public class MatchProjectionEntityConfiguration : IEntityTypeConfiguration<MatchProjection>
    {
        public void Configure(EntityTypeBuilder<MatchProjection> builder)
        {
            builder.ToView("vw_Match");

            builder.Property(x => x.EloRatingGainedForWinner).HasPrecision(10, 6);
            builder.Property(x => x.TotalEloRatingBeforeGameForWinner).HasPrecision(10, 6);
            builder.Property(x => x.TotalEloRatingAfterGameForWinner).HasPrecision(10, 6);
            builder.Property(x => x.WinChanceBasedOnEloRatingForWinner).HasPrecision(5, 4);

            builder.Property(x => x.EloRatingLostForLoser).HasPrecision(10, 6);
            builder.Property(x => x.TotalEloRatingBeforeGameForLoser).HasPrecision(10, 6);
            builder.Property(x => x.TotalEloRatingAfterGameForLoser).HasPrecision(10, 6);
            builder.Property(x => x.WinChanceBasedOnEloRatingForLoser).HasPrecision(5, 4);
        }
    }
}