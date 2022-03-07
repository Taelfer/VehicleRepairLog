using FluentValidation;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;

namespace VehicleRepairLog.ApplicationServices.API.Validators.Repairs
{
    public class AddRepairRequestValidator : AbstractValidator<AddRepairRequest>
    {
        public AddRepairRequestValidator()
        {
            RuleFor(repair => repair.Date).NotEmpty();
            RuleFor(repair => repair.Description).MaximumLength(1000);
            RuleFor(repair => repair.CarWorkshopName).MaximumLength(1000).NotEmpty();
            RuleFor(repair => repair.VehicleId).NotEmpty();
        }
    }
}
