using VehicleRepairLog.ApplicationServices.API.Domain.Models;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users
{
    public class ValidateUserResponse : ResponseBase<UserValidationDto>
    {
        public string Token { get; set; }
    }
}
