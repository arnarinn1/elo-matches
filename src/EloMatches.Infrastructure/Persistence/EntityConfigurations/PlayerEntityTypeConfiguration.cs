using System;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloMatches.Infrastructure.Persistence.EntityConfigurations
{
    public class PlayerEntityTypeConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Player");

            builder.HasKey("Id");
            builder.Property<Guid>("Id").HasColumnName("Id");

            builder.OwnsOne(typeof(Name), "UserName", buildAction => buildAction.Property<string>("Value").HasColumnName("UserName"));
            builder.OwnsOne(typeof(Name), "DisplayName", buildAction => buildAction.Property<string>("Value").HasColumnName("DisplayName"));
            
            builder.OwnsOne(typeof(EmailAddress), "Email", buildAction => buildAction.Property<string>("Value").HasColumnName("Email"));

            builder.OwnsOne(typeof(PlayerActiveStatus), "ActiveStatus", buildAction =>
            {
                buildAction.Property<DateTime?>("ActiveSince").HasColumnName("ActiveSince");
                buildAction.Property<DateTime?>("DeactivatedSince").HasColumnName("DeactivatedSince");
            });

            builder.Property<DateTime>("EntryDate").HasColumnName("EntryDate");

            builder.Ignore(b => b.DomainEvents);
        }
    }
}