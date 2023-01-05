using FluentValidation;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Users.ChangeUserPassword
{
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator(VehicleProfileStorageContext dbContext)
        {
            RuleFor(user => user.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);

            RuleFor(user => user.ConfirmPassword).Equal(user => user.NewPassword);
        }
    }
}
