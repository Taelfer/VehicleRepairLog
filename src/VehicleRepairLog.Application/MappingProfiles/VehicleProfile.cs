using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Features.Vehicles;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.MappingProfiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(vehicleDto => vehicleDto.Id, y => y.MapFrom(vehicle => vehicle.Id))
                .ForMember(vehicleDto => vehicleDto.BrandName, y => y.MapFrom(vehicle => vehicle.BrandName))
                .ForMember(vehicleDto => vehicleDto.VinNumber, y => y.MapFrom(vehicle => vehicle.VinNumber))
                .ForMember(vehicleDto => vehicleDto.PaintColor, y => y.MapFrom(vehicle => vehicle.PaintColor))
                .ForMember(vehicleDto => vehicleDto.FuelType, y => y.MapFrom(vehicle => vehicle.FuelType))
                .ForMember(vehicleDto => vehicleDto.Mileage, y => y.MapFrom(vehicle => vehicle.Mileage))
                .ForMember(vehicleDto => vehicleDto.UserId, y => y.MapFrom(vehicle => vehicle.UserId))
                .ForMember(vehicleDto => vehicleDto.RepairDescriptions, 
                                            y => y.MapFrom(vehicle => vehicle.Repairs != null ? 
                                                vehicle.Repairs.Select(repair => repair.Description) : new List<string>()));

            CreateMap<AddVehicleCommand, Vehicle>()
                .ForMember(vehicle => vehicle.BrandName, y => y.MapFrom(addVehicleCommand => addVehicleCommand.BrandName))
                .ForMember(vehicle => vehicle.VinNumber, y => y.MapFrom(addVehicleCommand => addVehicleCommand.VinNumber))
                .ForMember(vehicle => vehicle.PaintColor, y => y.MapFrom(addVehicleCommand => addVehicleCommand.PaintColor))
                .ForMember(vehicle => vehicle.FuelType, y => y.MapFrom(addVehicleCommand => addVehicleCommand.FuelType))
                .ForMember(vehicle => vehicle.Mileage, y => y.MapFrom(addVehicleCommand => addVehicleCommand.Mileage))
                .ForMember(vehicle => vehicle.UserId, y => y.MapFrom(addVehicleCommand => addVehicleCommand.UserId));

            CreateMap<UpdateVehicleCommand, Vehicle>()
                .ForMember(vehicle => vehicle.BrandName, y => y.MapFrom(updateVehicleCommand => updateVehicleCommand.BrandName))
                .ForMember(vehicle => vehicle.VinNumber, y => y.MapFrom(updateVehicleCommand => updateVehicleCommand.VinNumber))
                .ForMember(vehicle => vehicle.PaintColor, y => y.MapFrom(updateVehicleCommand => updateVehicleCommand.PaintColor))
                .ForMember(vehicle => vehicle.FuelType, y => y.MapFrom(updateVehicleCommand => updateVehicleCommand.FuelType))
                .ForMember(vehicle => vehicle.Mileage, y => y.MapFrom(updateVehicleCommand => updateVehicleCommand.Mileage));
        }
    }
}
