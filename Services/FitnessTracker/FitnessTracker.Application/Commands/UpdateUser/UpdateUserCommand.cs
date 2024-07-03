using AutoMapper;
using FitnessTracker.Application.Common.Interfaces;
using FitnessTracker.Application.Common.Models;
using FitnessTracker.Domain.Exceptions;
using UserEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.User;

namespace FitnessTracker.Application.Commands.UpdateUser
{
    public class UpdateUserCommand : UpdateUserRequest, ICommand<Guid>
    {
    }
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IFitnessTrackerRepository _repository;

        public UpdateUserCommandHandler(IFitnessTrackerRepository fitnessTrackerRepository, IConfigurationProvider configuration)
        {
            ArgumentNullException.ThrowIfNull(fitnessTrackerRepository);
            ArgumentNullException.ThrowIfNull(configuration);

            _repository = fitnessTrackerRepository;
            _mapper = configuration.CreateMapper();
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.FindAsync(e => e.Id == request.UserId, cancellationToken: cancellationToken);

            if (user is UserEntity)
            {
                user.Update(request.Name, 
                    request.Height, request.Weight,request.BirthDate,
                    request.EmailAddress, request.Address);

                await _repository.UpdateAsync(user, cancellationToken);
                await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            else
            {
                throw new UserNotFoundException(request.UserId);
            }

            return request.UserId;
        }
    }
}
