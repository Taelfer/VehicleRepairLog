using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users
{
    public class LoginUserRequest : IRequest<LoginUserResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
