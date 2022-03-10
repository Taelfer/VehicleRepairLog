using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;

namespace VehicleRepairLog.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersRequest request)
        {
            return this.HandleRequest<GetAllUsersRequest, GetAllUsersResponse>(request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            return this.HandleRequest<RegisterUserRequest, RegisterUserResponse>(request);
        }
    }
}
