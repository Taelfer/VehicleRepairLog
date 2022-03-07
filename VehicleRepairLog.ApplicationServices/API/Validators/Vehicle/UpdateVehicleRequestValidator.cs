using FluentValidation;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;

namespace VehicleRepairLog.ApplicationServices.API.Validators.Vehicle
{
    public class UpdateVehicleRequestValidator : AbstractValidator<UpdateVehicleRequest>
    {
        public UpdateVehicleRequestValidator()
        {
            RuleFor(vehicle => vehicle.BrandName).MaximumLength(100);
            RuleFor(vehicle => vehicle.VinNumber).MaximumLength(100).NotEmpty();
            RuleFor(vehicle => vehicle.PaintColor).MaximumLength(100);
            RuleFor(vehicle => vehicle.FuelType).MaximumLength(100);
            RuleFor(vehicle => vehicle.Mileage).InclusiveBetween(0, 10000000);
        }
    }
}
