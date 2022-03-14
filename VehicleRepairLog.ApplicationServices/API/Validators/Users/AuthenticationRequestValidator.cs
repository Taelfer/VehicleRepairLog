using FluentValidation;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;

namespace VehicleRepairLog.ApplicationServices.API.Validators.Users
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator()
        {
            //RuleFor(x => x.Username).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
