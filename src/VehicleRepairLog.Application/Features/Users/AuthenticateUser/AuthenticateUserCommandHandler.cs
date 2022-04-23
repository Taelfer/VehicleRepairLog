using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Authentication;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.Features.Users
{
    public class AuthenticateUserCommand : IRequest<string>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, string>
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

        public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
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

            string token = jwtAuth.GenerateToken(user);

            if (token is null)
            {
                throw new UnauthorizedException("You have to log in.");
            }

            return token;
        }
    }
}
