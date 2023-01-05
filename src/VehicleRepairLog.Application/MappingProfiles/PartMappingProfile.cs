using AutoMapper;
using VehicleRepairLog.Application.Features.Parts;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.MappingProfiles
{
    public class PartMappingProfile : Profile
    {
        public PartMappingProfile()
        {
            CreateMap<Part, PartDto>()
                .ForMember(partDto => partDto.Id, y => y.MapFrom(part => part.Id))
                .ForMember(partDto => partDto.Name, y => y.MapFrom(part => part.Name))
                .ForMember(partDto => partDto.BrandName, y => y.MapFrom(part => part.BrandName))
                .ForMember(partDto => partDto.Price, y => y.MapFrom(part => part.Price));

            CreateMap<AddPartCommand, Part>()
                .ForMember(part => part.Name, y => y.MapFrom(addPartCommand => addPartCommand.Name))
                .ForMember(part => part.BrandName, y => y.MapFrom(addPartCommand => addPartCommand.BrandName))
                .ForMember(part => part.Price, y => y.MapFrom(addPartCommand => addPartCommand.Price));

            CreateMap<UpdatePartCommand, Part>()
                .ForMember(part => part.Name, y => y.MapFrom(updatePartCommand => updatePartCommand.Name))
                .ForMember(part => part.BrandName, y => y.MapFrom(updatePartCommand => updatePartCommand.BrandName))
                .ForMember(part => part.Price, y => y.MapFrom(updatePartCommand => updatePartCommand.Price));
        }
    }
}
