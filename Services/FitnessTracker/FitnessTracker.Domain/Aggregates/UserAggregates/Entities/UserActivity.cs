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
            Location = userActivity.Location;
            TimeStarted = userActivity.TimeStarted;
            TimeEnded = userActivity.TimeEnded;
            Duration = userActivity.Duration;
            Distance = userActivity.Distance;
            AveragePace = userActivity.AveragePace;
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
            TimeSpan timeSpan = TimeStarted - TimeEnded;
            
            this.Duration = timeSpan.TotalHours;
        }

        public void CalculateAveragePace()
        {
            this.AveragePace = Math.Round((this.Duration / this.Distance), 2);
        }
    }
}
