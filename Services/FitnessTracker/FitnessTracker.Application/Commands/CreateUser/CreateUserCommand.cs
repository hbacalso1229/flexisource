using AutoMapper;
using FitnessTracker.Application.Common.Interfaces;
using FitnessTracker.Application.Common.Models;
using UserEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.User;

namespace FitnessTracker.Application.Commands.CreateUser
{
    public class CreateUserCommand : CreateUserRequest, ICommand<Guid>
    {
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IFitnessTrackerRepository _repository;

        public CreateUserCommandHandler(IFitnessTrackerRepository fitnessTrackerRepository, IConfigurationProvider configuration)
        {
            ArgumentNullException.ThrowIfNull(fitnessTrackerRepository);
            ArgumentNullException.ThrowIfNull(configuration);

            _repository = fitnessTrackerRepository;
            _mapper = configuration.CreateMapper();
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = _mapper.Map<UserEntity>(request);
            user.CalculateAge();
            user.CalculateBodyMassIndex();

            await _repository.AddAsync(user, cancellationToken);

            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return user.Id;
        }
    }
}
