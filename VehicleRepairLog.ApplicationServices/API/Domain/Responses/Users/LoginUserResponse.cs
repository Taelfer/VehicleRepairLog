using VehicleRepairLog.ApplicationServices.API.Domain.Models;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users
{
    public class LoginUserResponse : ResponseBase<LoginUserDto>
    {
        public string Token { get; set; }
    }
}
