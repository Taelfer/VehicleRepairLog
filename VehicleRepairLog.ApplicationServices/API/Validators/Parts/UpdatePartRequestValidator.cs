using FluentValidation;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Validators.Parts
{
    public class UpdatePartRequestValidator : AbstractValidator<UpdatePartRequest>
    {
        public UpdatePartRequestValidator()
        {
            RuleFor(part => part.Name).MaximumLength(100).NotEmpty();
            RuleFor(part => part.BrandName).MaximumLength(100).NotEmpty();
            RuleFor(part => part.Price).NotEmpty();
        }
    }
}
