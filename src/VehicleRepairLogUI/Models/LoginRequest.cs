using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLogUI.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email Address is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
