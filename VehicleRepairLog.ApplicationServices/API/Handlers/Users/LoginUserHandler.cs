using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Users;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Users
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IConfiguration configuration;

        public LoginUserHandler(IMapper mapper, IQueryExecutor queryExecutor, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
            this.passwordHasher = passwordHasher;
            this.configuration = configuration;
        }

        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var query = new LoginUserQuery()
            {
                Username = request.Username
            };
            var user = await this.queryExecutor.Execute(query);

            if (user is null)
            {
                return new LoginUserResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var verifiedPassword = this.passwordHasher
                .VerifyHashedPassword(user, user.Password, request.Password);

            if (verifiedPassword is PasswordVerificationResult.Failed)
            {
                return new LoginUserResponse()
                {
                    Error = new ErrorModel(ErrorType.NotAuthenticated)
                };
            }

            string token = null;

            if (user is not null)
            {
                token = GenerateToken(user);
            }

            return new LoginUserResponse()
            {
                Token = token
            };
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
