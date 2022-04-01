using FluentValidation;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class UpdatePartCommandValidator : AbstractValidator<UpdatePartCommand>
    {
        public UpdatePartCommandValidator()
        {
            RuleFor(part => part.Name).MaximumLength(100).NotEmpty();
            RuleFor(part => part.BrandName).MaximumLength(100).NotEmpty();
            RuleFor(part => part.Price).NotEmpty();
        }
    }
}
