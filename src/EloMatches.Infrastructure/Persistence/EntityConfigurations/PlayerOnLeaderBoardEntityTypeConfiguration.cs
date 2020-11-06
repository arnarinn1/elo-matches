using EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloMatches.Infrastructure.Persistence.EntityConfigurations
{
    public class PlayerOnLeaderBoardEntityTypeConfiguration : IEntityTypeConfiguration<PlayerOnLeaderBoard>
    {
        public void Configure(EntityTypeBuilder<PlayerOnLeaderBoard> builder)
        {
            builder.ToTable("PlayerLeaderBoard");

            builder.HasKey(x => x.PlayerId);
            builder.Property(x => x.PlayerId).HasColumnName("Id");

            builder.Property(x => x.EloRating).HasColumnName("EloRating").HasPrecision(10, 6);
            builder.Property(x => x.Rank).HasColumnName("Rank");
        }
    }
}