using FluentValidation;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;

namespace VehicleRepairLog.ApplicationServices.API.Validators.Users
{
    public class ValidateUserRequestValidator : AbstractValidator<ValidateUserRequest>
    {
        public ValidateUserRequestValidator()
        {
            //RuleFor(x => x.Username).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
