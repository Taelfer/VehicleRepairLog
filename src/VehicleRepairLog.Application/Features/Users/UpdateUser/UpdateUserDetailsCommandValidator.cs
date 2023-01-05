using FluentValidation;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Users.UpdateUser
{
    public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserDetailsCommandValidator(VehicleProfileStorageContext dbContext)
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
