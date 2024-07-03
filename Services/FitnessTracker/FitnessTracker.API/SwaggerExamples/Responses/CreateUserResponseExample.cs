using Swashbuckle.AspNetCore.Filters;

namespace FitnessTracker.API.SwaggerExamples.Responses
{
    public class CreateUserResponseExample : IExamplesProvider<Guid>
    {
        public Guid GetExamples()
        {
            return Guid.NewGuid();
        }
    }
}
