using EloMatches.Query.Projections.Matches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloMatches.Query.Persistence.EntityConfigurations
{
    public class MatchProjectionEntityConfiguration : IEntityTypeConfiguration<MatchProjection>
    {
        public void Configure(EntityTypeBuilder<MatchProjection> builder)
        {
            builder.ToTable("vw_Match");
        }
    }
}