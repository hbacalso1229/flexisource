using FitnessTracker.Application.Common.Models;
using Swashbuckle.AspNetCore.Filters;

namespace FitnessTracker.API.SwaggerExamples.Responses
{
    public class UserActivityResponseExample : IExamplesProvider<UserActivityResponse>
    {
        public UserActivityResponse GetExamples()
        {
            return new UserActivityResponse()
            {
                User = new UserDto
                {
                    Name = "Hendrix Axl",
                    BirthDate = DateTime.Now,
                    Height = 50,
                    Weight = 6,
                    Activities = new List<UserActivityDto>
                    {
                        new UserActivityDto
                        {
                            AveragePace = 8,
                            Distance = 9,
                            Duration = 10,
                            Location = "Makati City",
                            TimeEnded = DateTime.Now,
                            TimeStarted = DateTime.Now
                        }
                    }
                }
            };
        }
    }
}
