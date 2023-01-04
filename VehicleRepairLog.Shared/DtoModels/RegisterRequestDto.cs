using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Shared.DtoModels
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Email Address is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm your password.")]
        public string ConfirmPassword { get; set; }
    }
}
