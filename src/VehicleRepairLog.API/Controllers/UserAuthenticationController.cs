using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Users;
using VehicleRepairLog.Application.Models;

namespace VehicleRepairLog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserAuthenticationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserAuthenticationDto userAuthenticationDto)
        {
            var command = new AuthenticateUserCommand()
            {
                Username = userAuthenticationDto.Username,
                Password = userAuthenticationDto.Password,
                //Email = userAuthenticationDto.Email
            };

            var response = await this.mediator.Send(command);
            return this.Ok(response);
        }
    }
}
