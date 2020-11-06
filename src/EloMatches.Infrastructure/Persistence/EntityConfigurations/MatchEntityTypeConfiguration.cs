using EloMatches.Domain.AggregateModels.MatchesAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloMatches.Infrastructure.Persistence.EntityConfigurations
{
    public class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Match");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.OwnsOne(x => x.MatchResult, buildAction =>
            {
                buildAction.Property(x => x.WinnerScore).HasColumnName("WinnerScore");
                buildAction.Property(x => x.LoserScore).HasColumnName("LoserScore");
            });

            builder.OwnsOne(x => x.Winner, buildAction =>
            {
                buildAction.Property(x => x.PlayerId).HasColumnName("PlayerIdOfWinner");
                buildAction.Property(x => x.EloRatingGameDifference).HasColumnName("EloRatingGainedForWinner").HasPrecision(10, 6);
                buildAction.Property(x => x.TotalEloRatingBeforeGame).HasColumnName("TotalEloRatingBeforeGameForWinner").HasPrecision(10, 6);
                buildAction.Property(x => x.TotalEloRatingAfterGame).HasColumnName("TotalEloRatingAfterGameForWinner").HasPrecision(10, 6);
                buildAction.Property(x => x.ChanceOfWinning).HasColumnName("WinChanceBasedOnEloRatingForWinner").HasPrecision(5, 4);
                buildAction.Property(x => x.GameNumber).HasColumnName("WinnerGameNumber");
            });
            
            builder.OwnsOne(x => x.Loser, buildAction =>
            {
                buildAction.Property(x => x.PlayerId).HasColumnName("PlayerIdOfLoser");
                buildAction.Property(x => x.EloRatingGameDifference).HasColumnName("EloRatingLostForLoser").HasPrecision(10, 6);
                buildAction.Property(x => x.TotalEloRatingBeforeGame).HasColumnName("TotalEloRatingBeforeGameForLoser").HasPrecision(10, 6);
                buildAction.Property(x => x.TotalEloRatingAfterGame).HasColumnName("TotalEloRatingAfterGameForLoser").HasPrecision(10, 6);
                buildAction.Property(x => x.ChanceOfWinning).HasColumnName("WinChanceBasedOnEloRatingForLoser").HasPrecision(5, 4);
                buildAction.Property(x => x.GameNumber).HasColumnName("LoserGameNumber");
            });

            builder.OwnsOne(x => x.MatchDate, buildAction =>
            {
                buildAction.Property(x => x.Value).HasColumnName("MatchDate");
            });
        }
    }
}