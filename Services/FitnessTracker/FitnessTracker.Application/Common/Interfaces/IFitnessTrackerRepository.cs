using FitnessTracker.Domain.Aggregates.UserAggregates.Entities;
using UserActivityEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.UserActivity;

namespace FitnessTracker.Application.Common.Interfaces
{
    public interface IFitnessTrackerRepository : IRepository<User>
    {
        Task<User> AddUserActivityAsync(Guid userId, IList<UserActivityEntity> userActivities, CancellationToken cancellationToken);

        Task<User> GetUserActivityAsync(Guid userId, CancellationToken cancellationToken);
    }
}
