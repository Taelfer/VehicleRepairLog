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
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.Features.Users
{
    public class ValidateUserCommand : IRequest<TokenDto>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ValidateUserCommandHandler : IRequestHandler<ValidateUserCommand, TokenDto>
    {
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IConfiguration configuration;
        private readonly VehicleProfileStorageContext context;

        public ValidateUserCommandHandler(IPasswordHasher<User> passwordHasher, IConfiguration configuration, VehicleProfileStorageContext context)
        {
            this.passwordHasher = passwordHasher;
            this.configuration = configuration;
            this.context = context;
        }

        public async Task<TokenDto> Handle(ValidateUserCommand request, CancellationToken cancellationToken)
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
                throw new NotAuthenticatedException("User with such login and password does not exist.");
            }

            var verifiedPassword = this.passwordHasher
                .VerifyHashedPassword(user, user.Password, request.Password);

            if (verifiedPassword == PasswordVerificationResult.Failed)
            {
                throw new NotAuthenticatedException("Wrong login or password. Try again.");
            }

            string token = null;

            if (user is not null)
            {
                token = GenerateToken(user);
            }

            return new TokenDto()
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
