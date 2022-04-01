using FluentValidation;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class GetAllPartsQueryValidator : AbstractValidator<GetAllPartsQuery>
    {
        public GetAllPartsQueryValidator()
        {
            RuleFor(part => part.Name).MaximumLength(100);
        }
    }
}
