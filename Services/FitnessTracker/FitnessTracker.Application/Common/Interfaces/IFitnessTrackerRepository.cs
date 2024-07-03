using FitnessTracker.Domain.Aggregates.UserAggregates.Entities;

namespace FitnessTracker.Application.Common.Interfaces
{
    public interface IFitnessTrackerRepository : IRepository<User>
    {
        Task<User> AddUserActivityAsync(User user, CancellationToken cancellationToken);

        Task<User> GetUserActivityAsync(Guid userId, CancellationToken cancellationToken);
    }
}
