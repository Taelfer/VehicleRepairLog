using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Shared.DtoModels
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "User role is required.")]
        public string Role { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<string> Vehicles { get; set; }
    }
}
