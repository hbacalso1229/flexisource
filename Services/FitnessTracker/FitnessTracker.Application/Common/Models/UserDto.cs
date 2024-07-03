using AutoMapper;
using FitnessTracker.Application.Common.Interfaces;
using UserEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.User;

namespace FitnessTracker.Application.Common.Models
{
    public class UserDto : IMapFrom<UserEntity>
    {
        public UserDto()
        {
            Activities = new List<UserActivityDto>();
        }

        public string Name { get; set; }

        /// <summary>
        /// Height (cm)
        /// </summary>        
        public decimal Height { get; set; }

        /// <summary>
        /// Weight (kg)
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Birth Date
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// User Activities
        /// </summary>
        public IList<UserActivityDto> Activities { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserEntity, UserDto>();
            profile.CreateMap<string, string>().ConvertUsing(x => x ?? String.Empty);
        }
    }
}
