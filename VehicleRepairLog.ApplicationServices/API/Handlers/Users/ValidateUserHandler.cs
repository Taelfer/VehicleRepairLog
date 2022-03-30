using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Users
{
    public class ValidateUserHandler : IRequestHandler<ValidateUserRequest, ValidateUserResponse>
    {
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IConfiguration configuration;
        private readonly VehicleProfileStorageContext context;

        public ValidateUserHandler(IPasswordHasher<User> passwordHasher, IConfiguration configuration, VehicleProfileStorageContext context)
        {
            this.passwordHasher = passwordHasher;
            this.configuration = configuration;
            this.context = context;
        }

        public async Task<ValidateUserResponse> Handle(ValidateUserRequest request, CancellationToken cancellationToken)
        {
            User user = null;

            if (request.Username is not null)
            {
                user = await this.context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
            }
            else if(request.Email is not null)
            {
                user = await this.context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            }

            if (user is null)
            {
                return new ValidateUserResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var verifiedPassword = this.passwordHasher
                .VerifyHashedPassword(user, user.Password, request.Password);

            if (verifiedPassword == PasswordVerificationResult.Failed)
            {
                return new ValidateUserResponse()
                {
                    Error = new ErrorModel(ErrorType.NotAuthenticated)
                };
            }

            string token = null;

            if (user is not null)
            {
                token = GenerateToken(user);
            }

            return new ValidateUserResponse()
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
                new Claim(ClaimTypes.Role, user.Role.ToString()),
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
