using FluentValidation;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Validators
{
    public class AddPartRequestValidator : AbstractValidator<AddPartRequest>
    {
        public AddPartRequestValidator()
        {
            RuleFor(part => part.Name).NotNull().NotEmpty();
        }
    }
}
