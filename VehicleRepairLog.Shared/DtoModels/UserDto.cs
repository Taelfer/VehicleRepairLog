using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Shared.DtoModels
{
    public class UserDto
    {
        public int Id { get; set; }

        [MaxLength(ErrorMessage = "Firstname can have maximum 50 characters.")]
        public string FirstName { get; set; }

        [MaxLength(ErrorMessage = "Lastname can have maximum 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "User role is required.")]
        public string Role { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
