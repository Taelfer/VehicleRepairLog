using VehicleRepairLog.ApplicationServices.API.Domain.Models;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users
{
    public class AuthenticationResponse : ResponseBase<AuthenticationDto>
    {
        public string Token { get; set; }
    }
}
