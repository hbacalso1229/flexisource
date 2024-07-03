using AutoMapper;
using FitnessTracker.Application.Common.Interfaces;
using FitnessTracker.Application.Common.Models;
using UserActivityEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.UserActivity;

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
            IList<UserActivityEntity> userActivities = _mapper.Map<List<UserActivityEntity>>(request.UserActivities);              

            await _repository.AddUserActivityAsync(request.UserId, userActivities, cancellationToken);                  

            return request.UserId;
        }
    }
}
