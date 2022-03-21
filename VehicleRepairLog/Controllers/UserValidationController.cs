using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;

namespace VehicleRepairLog.Controllers
{
    [Route("")]
    [ApiController]
    public class UserValidationController : ApiControllerBase
    {
        public UserValidationController(IMediator mediator) : base(mediator) { }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public Task<IActionResult> AuthenticateUser([FromBody] UserValidationDto loginUserDto)
        {
            var request = new ValidateUserRequest()
            {
                Username = loginUserDto.Username,
                Password = loginUserDto.Password,
                Email = loginUserDto.Email
            };

            return this.HandleRequest<ValidateUserRequest, ValidateUserResponse>(request);
        }
    }
}
