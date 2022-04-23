using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Users;

namespace VehicleRepairLog.Controllers
{
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            await this.mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        [Route("{userId}")]
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
