using FitnessTracker.Application.Commands.CreateUser;
using FitnessTracker.Application.Commands.UpdateUser;
using Swashbuckle.AspNetCore.Filters;

namespace FitnessTracker.API.SwaggerExamples.Requests
{
    public class UpdateUserRequestExample : IExamplesProvider<UpdateUserCommand>
    {
        public UpdateUserCommand GetExamples()
        {
            return new UpdateUserCommand
            {
                UserId = new Guid("08dc9b58-abff-482d-8eb3-e51d05e8b0b7"),
                Name = "Lorem Ipsum",
                BirthDate = DateTime.Parse("11/23/2022"),
                Address = "Makati City",
                Weight = 90,
                Height = 168,
                EmailAddress = "testuser@flexisource.com"
            };
        }
    }
}
