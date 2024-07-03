using AutoMapper;
using FitnessTracker.Application.Common.Interfaces;
using UserEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.User;

namespace FitnessTracker.Application.Common.Models
{
    public class UpdateUserRequest : UserRequestDto, IMapFrom<UserEntity>
    {
        /// <summary>
        /// User unique identifier
        /// </summary>
        public Guid UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserRequest, UserEntity>();
            profile.CreateMap<string, string>().ConvertUsing(x => x ?? String.Empty);
        }
    }
}
