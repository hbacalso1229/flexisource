using FitnessTracker.Domain.SeedWork;

namespace FitnessTracker.Domain.Aggregates.UserAggregates.Entities
{
    public class User : AuditableEntity, IAggregateRoot
    {
        public User()
        {
            LastModified = Created = DateTime.UtcNow;
            CreatedBy = LastModifiedBy = "api";
        }

        public User(string name,
            double height,
            double weight,
            DateTime birthDate,
            short age,
            double bmi,
            string emailAddress,
            string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Height = height;
            Weight = weight;
            BirthDate = birthDate;
            Age = age;
            BMI = bmi;
            EmailAddress = emailAddress;
            Address = address;

            LastModified = Created = DateTime.UtcNow;
            CreatedBy = LastModifiedBy = "api";
        }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Height (cm)
        /// </summary>        
        public double Height { get; private set; }

        /// <summary>
        /// Weight (kg)
        /// </summary>
        public double Weight { get; private set; }

        /// <summary>
        /// Birth Date
        /// </summary>
        public DateTime BirthDate { get; private set; }


        /// <summary>
        /// Age
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Body Mass Index
        /// </summary>
        public double BMI { get; private set; }

        /// <summary>
        /// Email Address
        /// </summary>
        public string EmailAddress { get; private set; }

        /// <summary>
        /// Residence Address
        /// </summary>
        public string Address { get; private set; }


        /// <summary>
        /// User activity collection
        /// </summary>
        private readonly List<UserActivity> _userActivities = new List<UserActivity>();
        public IReadOnlyCollection<UserActivity> Activities => _userActivities;


        public void CalculateAge()
        {
            this.Age = DateTime.Now.Year - BirthDate.Year;
        } 

        public void CalculateBodyMassIndex()
        {
            this.BMI = Math.Round(this.Weight / ((this.Height / 100) * (this.Height / 100)), 2);
        }

        public void AddActivity(Guid userId, UserActivity userActivity)
        {
            UserActivity userActivityEntity = _userActivities.AsQueryable()
                .Where(x => x.Id == userActivity.Id && x.UserId == userId)
                .FirstOrDefault();            

            if (userActivityEntity is UserActivity)
            {
                //TODO
            }
            else
            {
                userActivityEntity = new UserActivity(userId, userActivity);
                userActivityEntity.CalculateDuration();
                userActivityEntity.CalculateAveragePace();

                _userActivities.Add(userActivityEntity);
            }                      
        }

        //TODO:
        public void SetLastModified()
        {
            LastModified = DateTime.UtcNow;
        }

        public void Update(string name,
            double height,
            double weight,
            DateTime birthDate,
            string emailAddress,
            string address)
        {
            this.Name = name;
            this.Height = height;
            this.Weight = weight;
            this.BirthDate = birthDate;
            this.EmailAddress = emailAddress;
            this.Address = address;
            this.CalculateAge();
            this.CalculateBodyMassIndex();
            this.SetLastModified();
        }
    }
}
