using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Users;
using VehicleRepairLog.Application.Models;

namespace VehicleRepairLog.Controllers
{
    [Route("")]
    [ApiController]
    public class UserValidationController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserValidationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserValidationDto loginUserDto)
        {
            var command = new ValidateUserCommand()
            {
                Username = loginUserDto.Username,
                Password = loginUserDto.Password,
                Email = loginUserDto.Email
            };

            var response = await this.mediator.Send(command);
            return this.Ok(response);
        }
    }
}
