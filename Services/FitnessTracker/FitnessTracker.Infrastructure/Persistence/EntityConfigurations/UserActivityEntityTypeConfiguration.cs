using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Domain.Aggregates.UserAggregates.Entities;

namespace FitnessTracker.Infrastructure.Persistence.EntityConfigurations
{
    public class UserActivityEntityTypeConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder.ToTable(nameof(UserActivity));

            builder.HasKey(e => new { e.Id, e.UserId });

            builder.Property(e => e.Location)
                .HasColumnType("varchar")
                .HasMaxLength(150)
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
