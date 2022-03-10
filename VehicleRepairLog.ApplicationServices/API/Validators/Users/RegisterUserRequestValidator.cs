using FluentValidation;
using System.Linq;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;
using VehicleRepairLog.DataAccess;

namespace VehicleRepairLog.ApplicationServices.API.Validators.Users
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        private readonly VehicleProfileStorageContext context;

        public RegisterUserRequestValidator(VehicleProfileStorageContext context)
        {
            //Maybe add NotEmpty()
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = this.context.Users.Any(x => x.Email == value);

                    if (emailInUse)
                    {
                        context.AddFailure("Try other email address.");
                    }
                });

            RuleFor(x => x.Username)
                .Custom((value, context) =>
                {
                    var usernameInUse = this.context.Users.Any(x => x.Username == value);

                    if (usernameInUse)
                    {
                        context.AddFailure("This username is taken.");
                    }
                });
        }
    }
}
