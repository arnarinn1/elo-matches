using EloMatches.Query.Projections.PlayerRankings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloMatches.Query.Persistence.EntityConfigurations
{
    public class PlayerRankingProjectionEntityConfiguration : IEntityTypeConfiguration<PlayerRankingProjection>
    {
        public void Configure(EntityTypeBuilder<PlayerRankingProjection> builder)
        {
            builder.ToTable("vw_PlayerRanking");
        }
    }
}