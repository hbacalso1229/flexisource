using AutoMapper;
using FitnessTracker.Application.Common.Interfaces;
using FitnessTracker.Application.Common.Models;
using UserEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.User;

namespace FitnessTracker.Application.Queries
{
    public class GetUserActivityQuery : IQuery<UserActivityResponse>
    {
        public GetUserActivityQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }

    public class GetUserActivityQueryHandler : IQueryHandler<GetUserActivityQuery, UserActivityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFitnessTrackerRepository _repository;

        public GetUserActivityQueryHandler(IFitnessTrackerRepository fitnessTrackerRepository, AutoMapper.IConfigurationProvider configuration)
        {
            ArgumentNullException.ThrowIfNull(fitnessTrackerRepository);
            ArgumentNullException.ThrowIfNull(configuration);

            _repository = fitnessTrackerRepository;
            _mapper = configuration.CreateMapper();
        }

        public async Task<UserActivityResponse> Handle(GetUserActivityQuery request, CancellationToken cancellationToken)
        {
            UserActivityResponse response = new UserActivityResponse();

            //TODO: Use Dapper instead of EF
            UserEntity user = await _repository.GetUserActivityAsync(request.UserId, cancellationToken);

            if(user is UserEntity)
            {
                response.User = _mapper.Map<UserDto>(user);
            }

            return response;
        }
    }
}
