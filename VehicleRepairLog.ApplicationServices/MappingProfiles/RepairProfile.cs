using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;

namespace VehicleRepairLog.ApplicationServices.MappingProfiles
{
    public class RepairProfile : Profile
    {
        public RepairProfile()
        {
            CreateMap<DataAccess.Entities.Repair, API.Domain.Models.RepairDto>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Date, y => y.MapFrom(z => z.Date))
                .ForMember(x => x.Description, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.CarWorkshopName, y => y.MapFrom(z => z.CarWorkshopName))
                .ForMember(x => x.PartNames, y => y.MapFrom(z => z.Parts != null ? z.Parts.Select(x => x.Name) : new List<string>()));

            CreateMap<AddRepairRequest, DataAccess.Entities.Repair>()
                .ForMember(x => x.Date, y => y.MapFrom(z => z.Date))
                .ForMember(x => x.Description, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.CarWorkshopName, y => y.MapFrom(z => z.CarWorkshopName))
                .ForMember(x => x.VehicleId, y => y.MapFrom(z => z.VehicleId));

            CreateMap<UpdateRepairRequest, DataAccess.Entities.Repair>()
                .ForMember(x => x.Date, y => y.MapFrom(z => z.Date))
                .ForMember(x => x.Description, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.CarWorkshopName, y => y.MapFrom(z => z.CarWorkshopName))
                .ForMember(x => x.VehicleId, y => y.MapFrom(z => z.VehicleId));
        }
    }
}
