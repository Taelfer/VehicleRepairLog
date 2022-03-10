using AutoMapper;

namespace VehicleRepairLog.ApplicationServices.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DataAccess.Entities.User, API.Domain.Models.UserDto>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
                .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Username));
        }
    }
}
