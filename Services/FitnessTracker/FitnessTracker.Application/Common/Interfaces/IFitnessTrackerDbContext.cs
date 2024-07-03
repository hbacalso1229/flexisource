using FitnessTracker.Domain.Aggregates.UserAggregates.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Common.Interfaces
{
    public interface IFitnessTrackerDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserActivity> UserActivities { get; set; }
    }
}
