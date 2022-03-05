using FluentValidation;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Validators.Parts
{
    public class UpdatePartRequestValidator : AbstractValidator<UpdatePartRequest>
    {
        public UpdatePartRequestValidator()
        {
            RuleFor(part => part.Name).Length(1, 100).NotNull();
            RuleFor(part => part.BrandName).Length(1, 100).NotNull();
            RuleFor(part => part.Price).NotEmpty();
        }
    }
}
