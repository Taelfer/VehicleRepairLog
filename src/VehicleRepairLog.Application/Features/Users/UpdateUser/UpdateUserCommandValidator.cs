using FluentValidation;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Users.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(VehicleProfileStorageContext dbContext)
        {
            RuleFor(user => user.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(user => user.Username)
                .MaximumLength(100)
                .NotEmpty();
        }
    }
}
