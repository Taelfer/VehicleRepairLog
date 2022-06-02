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
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.VinNumber, y => y.MapFrom(z => z.VinNumber))
                .ForMember(x => x.PaintColor, y => y.MapFrom(z => z.PaintColor))
                .ForMember(x => x.FuelType, y => y.MapFrom(z => z.FuelType))
                .ForMember(x => x.Mileage, y => y.MapFrom(z => z.Mileage))
                .ForMember(x => x.UserId, y => y.MapFrom(z => z.UserId))
                .ForMember(x => x.RepairDescriptions, y => y.MapFrom(z => z.Repairs != null ? z.Repairs.Select(x => x.Description) : new List<string>()));

            CreateMap<AddVehicleCommand, Vehicle>()
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.VinNumber, y => y.MapFrom(z => z.VinNumber))
                .ForMember(x => x.PaintColor, y => y.MapFrom(z => z.PaintColor))
                .ForMember(x => x.FuelType, y => y.MapFrom(z => z.FuelType))
                .ForMember(x => x.Mileage, y => y.MapFrom(z => z.Mileage))
                .ForMember(x => x.UserId, y => y.MapFrom(z => z.UserId));

            CreateMap<UpdateVehicleCommand, Vehicle>()
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.VinNumber, y => y.MapFrom(z => z.VinNumber))
                .ForMember(x => x.PaintColor, y => y.MapFrom(z => z.PaintColor))
                .ForMember(x => x.FuelType, y => y.MapFrom(z => z.FuelType))
                .ForMember(x => x.Mileage, y => y.MapFrom(z => z.Mileage));
        }
    }
}
