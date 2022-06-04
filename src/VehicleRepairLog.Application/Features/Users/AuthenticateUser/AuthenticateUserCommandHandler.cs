using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Authentication;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.Features.Users
{
    public class AuthenticateUserCommand : IRequest<LoginResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, LoginResultDto>
    {
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IJwtAuth jwtAuth;
        private readonly VehicleProfileStorageContext context;

        public AuthenticateUserCommandHandler(IPasswordHasher<User> passwordHasher, IJwtAuth jwtAuth, VehicleProfileStorageContext context)
        {
            this.passwordHasher = passwordHasher;
            this.jwtAuth = jwtAuth;
            this.context = context;
        }

        public async Task<LoginResultDto> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            User user = null;

            if(request.Email is not null)
            {
                user = await this.context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            }

            if (user is null)
            {
                throw new NotAuthenticatedException("User with such username and password does not exist.");
            }

            var verifiedPassword = this.passwordHasher
                .VerifyHashedPassword(user, user.Password, request.Password);

            if (verifiedPassword == PasswordVerificationResult.Failed)
            {
                throw new NotAuthenticatedException("Wrong username or password. Try again.");
            }

            string token = jwtAuth.GenerateToken(user);

            if (token is null)
            {
                throw new UnauthorizedException("You have to log in.");
            }

            return new LoginResultDto()
            {
                Token = token,
                Successful = true
            };
        }
    }
}
