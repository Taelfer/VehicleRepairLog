using MediatR;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users
{
    public class ValidateUserRequest : IRequest<ValidateUserResponse>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
