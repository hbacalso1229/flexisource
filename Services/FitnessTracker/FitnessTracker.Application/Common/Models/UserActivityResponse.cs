using FitnessTracker.Domain.Aggregates.UserAggregates.Entities;

namespace FitnessTracker.Application.Common.Models
{
    public class UserActivityResponse
    {
        public UserActivityResponse()
        {
            User = new UserDto();
        }

        public UserDto User { get; set; }
    }
}
