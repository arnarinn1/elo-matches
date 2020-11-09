using EloMatches.Query.Projections.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloMatches.Query.Persistence.EntityConfigurations
{
    public class PlayerProjectionEntityConfiguration : IEntityTypeConfiguration<PlayerProjection>
    {
        public void Configure(EntityTypeBuilder<PlayerProjection> builder)
        {
            builder.ToView("vw_Player", "elo");
            builder.HasKey(x => x.PlayerId);
            builder.Property(x => x.PlayerId).HasColumnName("Id");
        }
    }
}