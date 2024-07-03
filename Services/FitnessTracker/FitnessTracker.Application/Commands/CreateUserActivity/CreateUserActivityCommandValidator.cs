using FitnessTracker.Application.Commands.CreateUserActivity;
using FluentValidation;

namespace FitnessTracker.Application.Commands.CreateUserActivity
{
    public class CreateUserActivityCommandValidator : AbstractValidator<CreateUserActivityCommand>
    {
        public CreateUserActivityCommandValidator()
        {

        }
    }
}
