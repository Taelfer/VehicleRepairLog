using FluentValidation;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class AddPartCommandValidator : AbstractValidator<AddPartCommand>
    {
        public AddPartCommandValidator()
        {
            RuleFor(part => part.Name).MaximumLength(100).NotEmpty();
            RuleFor(part => part.BrandName).MaximumLength(100).NotEmpty();
            RuleFor(part => part.RepairId).NotEmpty();
        }
    }
}
