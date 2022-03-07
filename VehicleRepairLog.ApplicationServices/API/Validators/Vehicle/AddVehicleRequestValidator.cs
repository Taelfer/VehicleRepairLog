using FluentValidation;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;

namespace VehicleRepairLog.ApplicationServices.API.Validators.Vehicle
{
    public class AddVehicleRequestValidator : AbstractValidator<AddVehicleRequest>
    {
        public AddVehicleRequestValidator()
        {
            RuleFor(vehicle => vehicle.BrandName).MaximumLength(100);
            RuleFor(vehicle => vehicle.VinNumber).MaximumLength(100).NotEmpty();
            RuleFor(vehicle => vehicle.PaintColor).MaximumLength(100);
            RuleFor(vehicle => vehicle.FuelType).MaximumLength(100);
            RuleFor(vehicle => vehicle.Mileage).InclusiveBetween(0, 10000000);
            RuleFor(vehicle => vehicle.UserId).NotEmpty();
        }
    }
}
