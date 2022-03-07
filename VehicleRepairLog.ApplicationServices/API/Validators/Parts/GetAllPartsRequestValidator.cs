using FluentValidation;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Validators.Parts
{
    public class GetAllPartsRequestValidator : AbstractValidator<GetAllPartsRequest>
    {
        public GetAllPartsRequestValidator()
        {
            RuleFor(part => part.Name).MaximumLength(100);
        }
    }
}
