
namespace FitnessTracker.Application.Common.Models
{
    public class UserRequestDto
    {
        public UserRequestDto() { }

        /// <summary>
        /// User name
        /// </summary>        
        public string Name { get; set; }

        /// <summary>
        /// Height (cm)
        /// </summary>        
        public double Height { get; set; }

        /// <summary>
        /// Weight (kg)
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Birth Date
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Residence Address
        /// </summary>
        public string Address { get; set; }
    }
}
