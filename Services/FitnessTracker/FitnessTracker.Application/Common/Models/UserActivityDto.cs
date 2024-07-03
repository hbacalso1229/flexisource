using AutoMapper;
using FitnessTracker.Application.Common.Interfaces;
using UserActivityEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.UserActivity;

namespace FitnessTracker.Application.Common.Models
{
    public class UserActivityDto : IMapFrom<UserActivityEntity>
    {
        public string Location { get; set; }

        public DateTime TimeStarted { get; set; }

        public DateTime TimeEnded { get; set; }

        public double Distance { get; set; }

        public double Duration { get; set; }

        public double AveragePace { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserActivityEntity, UserActivityDto>();
            profile.CreateMap<string, string>().ConvertUsing(x => x ?? String.Empty);
        }
    }
}
