using AutoMapper;
using VehicleRepairLog.Application.Features.Repairs;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.MappingProfiles
{
    public class RepairMappingProfile : Profile
    {
        public RepairMappingProfile()
        {
            CreateMap<Repair, RepairDto>()
                .ForMember(repairDto => repairDto.Id, y => y.MapFrom(repair => repair.Id))
                .ForMember(repairDto => repairDto.Date, y => y.MapFrom(repair => repair.Date))
                .ForMember(repairDto => repairDto.Name, y => y.MapFrom(repair => repair.Name))
                .ForMember(repairDto => repairDto.Description, y => y.MapFrom(repair => repair.Description))
                .ForMember(repairDto => repairDto.CarWorkshopName, y => y.MapFrom(repair => repair.CarWorkshopName));

            CreateMap<AddRepairCommand, Repair>()
                .ForMember(repair => repair.Date, y => y.MapFrom(addRepairCommand => addRepairCommand.Date))
                .ForMember(repair => repair.Description, y => y.MapFrom(addRepairCommand => addRepairCommand.Description))
                .ForMember(repair => repair.CarWorkshopName, y => y.MapFrom(addRepairCommand => addRepairCommand.CarWorkshopName))
                .ForMember(repair => repair.VehicleId, y => y.MapFrom(addRepairCommand => addRepairCommand.VehicleId));

            CreateMap<UpdateRepairCommand, Repair>()
                .ForMember(repair => repair.Date, y => y.MapFrom(updateRepairCommand => updateRepairCommand.Date))
                .ForMember(repair => repair.Description, y => y.MapFrom(updateRepairCommand => updateRepairCommand.Description))
                .ForMember(repair => repair.CarWorkshopName, y => y.MapFrom(updateRepairCommand => updateRepairCommand.CarWorkshopName))
                .ForMember(repair => repair.VehicleId, y => y.MapFrom(updateRepairCommand => updateRepairCommand.VehicleId));
        }
    }
}
