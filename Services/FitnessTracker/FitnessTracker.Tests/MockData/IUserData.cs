namespace FitnessTracker.Tests
{
    public interface IUserData<TResponse>
    {
        TResponse GetData();
    }
}
