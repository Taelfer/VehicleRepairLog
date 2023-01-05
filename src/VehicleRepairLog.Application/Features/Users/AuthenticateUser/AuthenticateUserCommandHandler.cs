using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Authentication;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Users
{
    public class AuthenticateUserCommand : IRequest<LoginResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, LoginResultDto>
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtAuth _jwtAuth;
        private readonly VehicleProfileStorageContext _context;

        public AuthenticateUserCommandHandler(IPasswordHasher<User> passwordHasher, IJwtAuth jwtAuth, VehicleProfileStorageContext context)
        {
            _passwordHasher = passwordHasher;
            _jwtAuth = jwtAuth;
            _context = context;
        }

        public async Task<LoginResultDto> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            User user = null;

            if(request.Email is not null)
            {
                user = await _context.Users.FirstOrDefaultAsync(user => user.Email == request.Email, cancellationToken);
            }

            if (user is null)
            {
                throw new NotAuthenticatedException("User with such username and password does not exist.");
            }

            PasswordVerificationResult verifiedPassword = _passwordHasher
                .VerifyHashedPassword(user, user.Password, request.Password);

            if (verifiedPassword == PasswordVerificationResult.Failed)
            {
                throw new NotAuthenticatedException("Wrong username or password. Try again.");
            }

            string token = _jwtAuth.GenerateToken(user);

            return new LoginResultDto()
            {
                Token = token,
                Successful = true
            };
        }
    }
}
