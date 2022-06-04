using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Users;
using VehicleRepairLog.Application.Models;

namespace VehicleRepairLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            var response = await this.mediator.Send(command);
            return this.Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginRequestDto loginRequest)
        {
            var command = new AuthenticateUserCommand()
            {
                Password = loginRequest.Password,
                Email = loginRequest.Email
            };

            var response = await this.mediator.Send(command);
            return this.Ok(response);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            var query = new GetUserByIdQuery()
            {
                UserId = userId
            };

            var response = await this.mediator.Send(query);
            return this.Ok(response);
        }
    }
}
