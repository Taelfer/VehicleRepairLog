using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
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
        private readonly IConfiguration _configuration;

        public UsersController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
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

        [Authorize(Roles = "User,Admin")]
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

        [HttpPost("validateJwt")]
        public ActionResult<LoginResultDto> ValidateJwt([FromBody] string token)
        {
            try
            {
                var secretKey = _configuration["Jwt:Key"];

                TokenValidationParameters tokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token))
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                var validationResult = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var validatedToken = (JwtSecurityToken)securityToken;

                if (validatedToken is not null && validatedToken.ValidTo > DateTime.Now)
                {
                    return new LoginResultDto()
                    {
                        IsAuthenticated = true
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);

                return new LoginResultDto()
                {
                    IsAuthenticated = false
                };
            }

            return null;
        }
    }
}
