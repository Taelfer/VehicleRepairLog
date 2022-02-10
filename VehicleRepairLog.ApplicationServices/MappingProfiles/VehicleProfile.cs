using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRepairLog.ApplicationServices.MappingProfiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<DataAccess.Entities.Vehicle, API.Domain.Models.Vehicle>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.VinNumber, y => y.MapFrom(z => z.VinNumber))
                .ForMember(x => x.PaintColor, y => y.MapFrom(z => z.PaintColor))
                .ForMember(x => x.FuelType, y => y.MapFrom(z => z.FuelType))
                .ForMember(x => x.Mileage, y => y.MapFrom(z => z.Mileage));
        }
    }
}
