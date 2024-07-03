using AutoMapper;
using FitnessTracker.Application.Common.Interfaces;
using FitnessTracker.Application.Common.Models;
using FitnessTracker.Domain.Exceptions;
using UserActivityEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.UserActivity;
using UserEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.User;

namespace FitnessTracker.Application.Commands.CreateUserActivity
{
    public class CreateUserActivityCommand : CreateUserActivityRequest, ICommand<Guid>
    {
    }

    public class CreateUserActivityCommandHandler : ICommandHandler<CreateUserActivityCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IFitnessTrackerRepository _repository;

        public CreateUserActivityCommandHandler(IFitnessTrackerRepository fitnessTrackerRepository, IConfigurationProvider configuration)
        {
            ArgumentNullException.ThrowIfNull(fitnessTrackerRepository);
            ArgumentNullException.ThrowIfNull(configuration);

            _repository = fitnessTrackerRepository;
            _mapper = configuration.CreateMapper();
        }

        public async Task<Guid> Handle(CreateUserActivityCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await _repository.FindAsync(e => e.Id == request.UserId, cancellationToken: cancellationToken);

            if (user is UserEntity)
            {
                IList<UserActivityEntity> userActivities = _mapper.Map<List<UserActivityEntity>>(request.UserActivities);

                user.AddActivities(request.UserId, userActivities.ToList());

                await _repository.AddUserActivityAsync(user, cancellationToken);
            }
            else
            {
                throw new UserNotFoundException(request.UserId);
            }            

            return request.UserId;
        }
    }
}
