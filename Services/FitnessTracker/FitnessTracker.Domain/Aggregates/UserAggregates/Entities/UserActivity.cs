using FitnessTracker.Domain.SeedWork;

namespace FitnessTracker.Domain.Aggregates.UserAggregates.Entities
{
    public class UserActivity : Entity
    {
        public UserActivity() { }

        public UserActivity(Guid userId, UserActivity userActivity, Guid id = new Guid())
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;

            UserId = userId;           
        }

        public Guid UserId { get; private set; }

        public string Location { get; private set; }

        public DateTime TimeStarted { get; private set; }

        public DateTime TimeEnded { get; private set; }

        public double Distance { get; private set; }

        public double Duration { get; private set; }

        public double AveragePace { get; private set; }

        public void UpdateUserActivity(UserActivity userActivity)
        {
            //TODO
        }

        public void CalculateDuration()
        {
            this.Duration = (TimeStarted - TimeEnded).TotalHours;
        }

        public void CalculateAveragePace()
        {
            this.AveragePace = (this.Duration / this.Distance);
        }
    }
}
