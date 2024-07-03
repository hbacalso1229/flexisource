using FitnessTracker.Application.Common.Interfaces;
using FitnessTracker.Domain.Exceptions;
using FitnessTracker.Infrastructure.Persistence.Base;
using FitnessTracker.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.User;

namespace Fitness.Infrastructure.Persistence.Repositories
{
    public class FitnessTrackerRepository : Repository<FitnessTrackerDbContext, UserEntity>, IFitnessTrackerRepository
    {
        public FitnessTrackerRepository(FitnessTrackerDbContext dbContext, ILogger<FitnessTrackerRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<UserEntity> AddUserActivityAsync(UserEntity user, CancellationToken cancellationToken)
        {
            UserEntity entity = await Context.Users.AsQueryable()
                .Where(x => x.Id == user.Id)
                .Include(x => x.Activities)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is UserEntity)
            {                
                entity.AddActivities(user.Activities.ToList());                

                await UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            else
            {
                throw new UserNotFoundException(user.Id);
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
