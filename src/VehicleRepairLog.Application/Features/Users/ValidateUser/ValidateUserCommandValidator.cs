using FluentValidation;

namespace VehicleRepairLog.Application.Features.Users
{
    public class ValidateUserCommandValidator : AbstractValidator<ValidateUserCommand>
    {
        public ValidateUserCommandValidator()
        {
            //RuleFor(x => x.Username).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
