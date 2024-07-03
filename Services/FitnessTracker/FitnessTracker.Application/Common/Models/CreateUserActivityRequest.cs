using AutoMapper;
using FitnessTracker.Application.Common.Interfaces;
using UserActivityEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.UserActivity;

namespace FitnessTracker.Application.Common.Models
{
    public class CreateUserActivityRequest
    {
        public CreateUserActivityRequest()
        {
            UserActivities = new List<CreateUserActivityDto>();
        }

        public Guid UserId { get; set; }

        public IList<CreateUserActivityDto> UserActivities { get; set; }
    }

    public class CreateUserActivityDto : IMapFrom<UserActivityEntity>
    {
        public CreateUserActivityDto() { }

        /// <summary>
        /// Location
        /// </summary>        
        public string Location { get; set; }

        /// <summary>
        /// Date Started
        /// </summary>        
        public DateTime DateStarted { get; set; }

        /// <summary>
        /// Date Ended
        /// </summary>        
        public DateTime DateEnded { get; set; }

        /// <summary>
        /// Distance
        /// </summary>        
        public double Distance { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserActivityDto, UserActivityEntity>()                    
                .ForMember(x => x.TimeEnded, opt => opt.MapFrom(x => x.DateEnded))
                .ForMember(x => x.TimeStarted, opt => opt.MapFrom(x => x.DateStarted));

            profile.CreateMap<string, string>().ConvertUsing(x => x ?? String.Empty);
        }
    }
}
