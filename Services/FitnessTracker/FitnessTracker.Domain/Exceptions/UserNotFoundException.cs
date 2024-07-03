namespace FitnessTracker.Domain.Exceptions
{
    public sealed class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid userId)
            : base($"The user with identifier '{userId.ToString()}' was not found.")
        {
        }
    }
}
