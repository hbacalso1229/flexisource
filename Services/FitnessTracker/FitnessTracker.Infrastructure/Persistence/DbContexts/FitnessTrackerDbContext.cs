using FitnessTracker.Application.Common.Interfaces;
using FitnessTracker.Domain.Aggregates.UserAggregates.Entities;
using FitnessTracker.Infrastructure.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FitnessTracker.Infrastructure.Persistence.DbContexts
{
    public class FitnessTrackerDbContext : BaseContext<FitnessTrackerDbContext>, IFitnessTrackerDbContext, IUnitOfWork
    {
        public FitnessTrackerDbContext(DbContextOptions<FitnessTrackerDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<UserActivity> UserActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}

