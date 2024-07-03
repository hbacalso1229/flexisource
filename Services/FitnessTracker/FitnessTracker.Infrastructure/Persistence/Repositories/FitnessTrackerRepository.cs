using FitnessTracker.Application.Common.Interfaces;
using FitnessTracker.Domain.Exceptions;
using FitnessTracker.Infrastructure.Persistence.Base;
using FitnessTracker.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.User;
using UserActivityEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.UserActivity;

namespace Fitness.Infrastructure.Persistence.Repositories
{
    public class FitnessTrackerRepository : Repository<FitnessTrackerDbContext, UserEntity>, IFitnessTrackerRepository
    {
        public FitnessTrackerRepository(FitnessTrackerDbContext dbContext, ILogger<FitnessTrackerRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<UserEntity> AddUserActivityAsync(Guid userId, IList<UserActivityEntity> userActivities, CancellationToken cancellationToken)
        {
            UserEntity entity = await Context.Users.AsQueryable()
                .Where(x => x.Id == userId)
                .Include(x => x.Activities)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is UserEntity)
            {
                entity.UpdateUser();

                userActivities.ToList().ForEach(activity =>
                {
                    entity.AddActivity(entity.Id, activity);
                });

                await UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            else
            {
                throw new UserNotFoundException(userId);
            }

            return entity;

        }

        public async Task<UserEntity> GetUserActivityAsync(Guid userId, CancellationToken cancellationToken)
        {
            UserEntity entity = await Context.Users.AsQueryable()
                .Where(x => x.Id == userId)
                .Include(x => x.Activities)
                .FirstOrDefaultAsync(cancellationToken);

            return entity ?? new UserEntity();
        }
    }
}
