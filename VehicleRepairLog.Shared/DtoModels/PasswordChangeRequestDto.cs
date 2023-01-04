using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Shared.DtoModels
{
    public class PasswordChangeRequestDto
    {
        [Required(ErrorMessage = "Password is required.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Password confirmation is required.")]
        public string ConfirmPassword { get; set; }
    }
}
