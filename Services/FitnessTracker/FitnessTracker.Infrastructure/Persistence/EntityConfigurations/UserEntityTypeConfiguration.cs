using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Domain.Aggregates.UserAggregates.Entities;

namespace FitnessTracker.Infrastructure.Persistence.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(e => new { e.Id });

            builder.Property(e => e.Name)
               .HasMaxLength(50)
               .HasDefaultValue(String.Empty)
               .IsRequired();

            builder.Property(e => e.Height)
               .IsRequired();

            builder.Property(e => e.Weight)
                .IsRequired();

            builder.Property(e => e.BirthDate)
                .IsRequired();

            builder.Property(e => e.Age);
            builder.Property(e => e.BMI);
            builder.Property(e => e.EmailAddress);
            builder.Property(e => e.Address);

            builder.HasMany(e => e.Activities)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
