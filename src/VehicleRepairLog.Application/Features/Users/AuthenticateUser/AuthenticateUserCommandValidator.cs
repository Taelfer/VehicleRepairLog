using FluentValidation;

namespace VehicleRepairLog.Application.Features.Users
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            //RuleFor(x => x.Username).NotEmpty();

            RuleFor(user => user.Password).NotEmpty();
        }
    }
}
