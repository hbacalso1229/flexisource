using FitnessTracker.Application.Commands.CreateUserActivity;
using FitnessTracker.Application.Common.Models;
using Swashbuckle.AspNetCore.Filters;

namespace FitnessTracker.API.SwaggerExamples.Requests
{
    public class CreateUserActivityRequestExample : IExamplesProvider<CreateUserActivityCommand>
    {
        public CreateUserActivityCommand GetExamples()
        {
            return new CreateUserActivityCommand
            {
                UserId = Guid.NewGuid(),
                UserActivities = new List<CreateUserActivityDto>
                {
                    new CreateUserActivityDto
                    {
                        DateEnded = DateTime.UtcNow,
                        DateStarted = DateTime.UtcNow,
                        Distance = 15,
                        Location = "Somewhere Else"
                    }
                }
            };
        }
    }
}
