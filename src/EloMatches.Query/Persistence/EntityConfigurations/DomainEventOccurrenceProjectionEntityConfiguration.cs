using EloMatches.Query.Projections.DomainEventOccurrences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloMatches.Query.Persistence.EntityConfigurations
{
    public class DomainEventOccurrenceProjectionEntityConfiguration : IEntityTypeConfiguration<DomainEventOccurrenceProjection>
    {
        public void Configure(EntityTypeBuilder<DomainEventOccurrenceProjection> builder)
        {
            builder.ToView("vw_DomainEventOccurrence", "elo");
        }
    }
}