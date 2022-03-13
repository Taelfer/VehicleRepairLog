using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;

namespace VehicleRepairLog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginUserController : ApiControllerBase
    {
        public LoginUserController(IMediator mediator) : base(mediator) { }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            var request = new LoginUserRequest()
            {
                Username = loginUserDto.Username,
                Password = loginUserDto.Password
            };

            return this.HandleRequest<LoginUserRequest, LoginUserResponse>(request);
        }
    }
}
