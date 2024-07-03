using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Domain.Aggregates.UserAggregates.Entities;

namespace FitnessTracker.Infrastructure.Persistence.EntityConfigurations
{
    public class UserActivityEntityTypeConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder.HasKey(e => new { e.Id });
            builder.HasIndex(e => new { e.Id, e.UserId }).IsUnique();

            builder.Property(e => e.Location)
                .HasDefaultValue(String.Empty)
                .IsRequired();

            builder.Property(e => e.TimeStarted)
                .IsRequired();

            builder.Property(e => e.TimeStarted)
                .IsRequired();

            builder.Property(e => e.Distance)
                .IsRequired();

            builder.Property(e => e.Duration);

            builder.Property(e => e.AveragePace);
        }
    }
}
