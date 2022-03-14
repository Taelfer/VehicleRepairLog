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
    public class AuthenticationController : ApiControllerBase
    {
        public AuthenticationController(IMediator mediator) : base(mediator) { }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public Task<IActionResult> AuthenticateUser([FromBody] AuthenticationDto loginUserDto)
        {
            var request = new AuthenticationRequest()
            {
                Username = loginUserDto.Username,
                Password = loginUserDto.Password,
                Email = loginUserDto.Email
            };

            return this.HandleRequest<AuthenticationRequest, AuthenticationResponse>(request);
        }
    }
}
