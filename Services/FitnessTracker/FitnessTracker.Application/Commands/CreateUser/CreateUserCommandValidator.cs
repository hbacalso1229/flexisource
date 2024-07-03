using FluentValidation;

namespace FitnessTracker.Application.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IServiceProvider serviceProvider)
        {
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("{PropertyName} maximum length no greater then 50 characters.")
               .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Height).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Weight).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.BirthDate).Must(IsValidBirthDate).WithMessage("Invalid {PropertyName}");
        }

        private bool IsValidBirthDate(DateTime dateOfBirth) 
        {
            int age = GetAge(dateOfBirth);

            if (age < 0 || age > 150) return false; 

            return true;
        }

        private int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Now;

            int age = today.Year - birthDate.Year;

            if (today.Month < birthDate.Month || (today.Month == birthDate.Month && today.Day < birthDate.Day)) { age--; }

            return age;
        }
    }
}
