using AutoMapper;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Features.Parts;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.MappingProfiles
{
    public class PartProfile : Profile
    {
        public PartProfile()
        {
            CreateMap<Part, PartDto>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price));

            CreateMap<AddPartCommand, Part>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price));

            CreateMap<UpdatePartCommand, Part>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price));
        }
    }
}
