using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public UserRole Role { get; set; }

        [Required]
        [MaxLength (100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
