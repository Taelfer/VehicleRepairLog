using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.MappingProfiles
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

            CreateMap<AddPartRequest, Part>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price));

            CreateMap<UpdatePartRequest, Part>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price));
        }
    }
}
