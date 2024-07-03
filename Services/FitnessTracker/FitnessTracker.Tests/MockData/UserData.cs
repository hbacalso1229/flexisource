using FitnessTracker.Application.Common.Models;

namespace FitnessTracker.Tests.MockData
{
    public class UserData : IUserData<UserActivityResponse>
    {
        public UserActivityResponse GetData()
        {
            Guid userId = Guid.NewGuid();

            return new UserActivityResponse
            {
                User = new UserDto
                {
                    Name = "Hendrix Axl",
                    BirthDate = DateTime.Now,
                    Height = 50,
                    Weight = 6,
                    Activities = new List<UserActivityDto>
                    {
                        new UserActivityDto
                        {
                            AveragePace = 8,
                            Distance = 9,
                            Duration = 10,
                            Location = "Makati City",
                            TimeEnded = DateTime.Now,
                            TimeStarted = DateTime.Now
                        }
                    }
                }
            };
        }
    }
}
