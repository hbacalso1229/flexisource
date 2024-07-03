namespace FitnessTracker.Application.Common.Exceptions
{
    public abstract class ServiceUnavailableException : ApplicationException
    {
        public ServiceUnavailableException(string message)
            : base("Service Unavailable", message)
        {
        }
    }
}
