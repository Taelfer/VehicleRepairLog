using FluentValidation;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;

namespace VehicleRepairLog.ApplicationServices.API.Validators.Users
{
    public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
