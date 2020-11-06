using EloMatches.Infrastructure.Persistence.CrudEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloMatches.Infrastructure.Persistence.EntityConfigurations
{
    public class DomainEventOccurrenceEntityTypeConfiguration : IEntityTypeConfiguration<DomainEventOccurrenceEntity>
    {
        public void Configure(EntityTypeBuilder<DomainEventOccurrenceEntity> builder)
        {
            builder.ToTable("DomainEventOccurrence");
        }
    }
}