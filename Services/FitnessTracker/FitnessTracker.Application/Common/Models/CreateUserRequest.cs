using AutoMapper;
using FitnessTracker.Application.Common.Interfaces;
using UserEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.User;

namespace FitnessTracker.Application.Common.Models
{
    public class CreateUserRequest : IMapFrom<UserEntity>
    {
        public CreateUserRequest() { }

        /// <summary>
        /// User name
        /// </summary>        
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
        /// Email Address
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Residence Address
        /// </summary>
        public string Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserRequest, UserEntity>();
            profile.CreateMap<string, string>().ConvertUsing(x => x ?? String.Empty);
        }
    }
}
