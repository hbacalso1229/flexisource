using FitnessTracker.Application.Commands.CreateUser;
using Swashbuckle.AspNetCore.Filters;

namespace FitnessTracker.API.SwaggerExamples.Requests
{
    public class CreateUserRequestExample : IExamplesProvider<CreateUserCommand>
    {
        public CreateUserCommand GetExamples()
        {
            return new CreateUserCommand
            {
                Name = "Lorem Ipsum Dolor",
                BirthDate = DateTime.Parse("11/23/2022"),
                Address = "Makati City",
                Weight = 90,
                Height = 168,
                EmailAddress = "hbacalso1229@yahoo.com"
            };
        }
    }
}
