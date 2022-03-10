using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;

namespace VehicleRepairLog.ApplicationServices.MappingProfiles
{
    public class PartProfile : Profile
    {
        public PartProfile()
        {
            CreateMap<DataAccess.Entities.Part, API.Domain.Models.PartDto>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price));

            CreateMap<AddPartRequest, DataAccess.Entities.Part>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price));

            CreateMap<UpdatePartRequest, DataAccess.Entities.Part>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.BrandName, y => y.MapFrom(z => z.BrandName))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price));
        }
    }
}
