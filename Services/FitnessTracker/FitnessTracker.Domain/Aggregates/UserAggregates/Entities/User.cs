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
            decimal height,
            decimal weight,
            DateTime birthDate,
            short age,
            decimal bmi,
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
        public decimal Height { get; private set; }

        /// <summary>
        /// Weight (kg)
        /// </summary>
        public decimal Weight { get; private set; }

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
        public decimal BMI { get; private set; }

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
            this.BMI = this.Weight / ((this.Height / 100) * (this.Height / 100));
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
        public void UpdateUser()
        {
            LastModified = DateTime.UtcNow;
        }
    }
}
