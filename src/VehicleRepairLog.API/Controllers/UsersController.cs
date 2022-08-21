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
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            RegisterResultDto response = await _mediator.Send(command);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginRequestDto loginRequest)
        {
            AuthenticateUserCommand command = new()
            {
                Password = loginRequest.Password,
                Email = loginRequest.Email
            };

            LoginResultDto response = await _mediator.Send(command);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            GetUserByIdQuery query = new()
            {
                UserId = userId
            };

            UserDto response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
