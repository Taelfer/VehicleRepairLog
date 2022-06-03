using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Users;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IConfiguration configuration;

        public UsersController(IMediator mediator, IConfiguration configuration)
        {
            this.mediator = mediator;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            await this.mediator.Send(command);
            return NoContent();
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

        //REFACTOR this endpoint. Create Handler for it.
        [HttpPost("getUserByJwt")]
        public async Task<IActionResult> GetUserByJwt([FromBody] string jwtToken)
        {
            try
            {
                //getting secret key
                string secretKey = this.configuration["Jwt:Key"];
                var key = Encoding.ASCII.GetBytes(secretKey);

                //validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                //validating token
                var principle = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = (JwtSecurityToken)securityToken;

                if (jwtSecurityToken is not null 
                    && jwtSecurityToken.ValidTo > DateTime.Now 
                    && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    //returning user if found
                    var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    return await GetUserById(Convert.ToInt32(userId));
                }
            }
            catch (Exception ex)
            {
                //logging the error and returning null
                Console.WriteLine("Exception : " + ex.Message);
                return null;
            }

            return null;
        }
    }
}
