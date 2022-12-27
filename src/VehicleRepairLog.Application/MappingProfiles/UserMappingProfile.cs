using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Features.Users;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Application.Features.Users.UpdateUser;

namespace VehicleRepairLog.Application.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(userDto => userDto.Id, y => y.MapFrom(user => user.Id))
                .ForMember(userDto => userDto.FirstName, y => y.MapFrom(user => user.FirstName))
                .ForMember(userDto => userDto.LastName, y => y.MapFrom(user => user.LastName))
                .ForMember(userDto => userDto.Username, y => y.MapFrom(user => user.Username))
                .ForMember(userDto => userDto.DateOfBirth, y => y.MapFrom(user => user.DateOfBirth))
                .ForMember(userDto => userDto.Email, y => y.MapFrom(user => user.Email))
                .ForMember(userDto => userDto.Vehicles, 
                                        y => y.MapFrom(user => user.Vehicles != null ?
                                            user.Vehicles.Select(vehicle => vehicle.BrandName) : new List<string>()));

            CreateMap<RegisterUserCommand, User>()
                .ForMember(user => user.Username, y => y.MapFrom(registerUserCommand => registerUserCommand.Username))
                .ForMember(user => user.Email, y => y.MapFrom(registerUserCommand => registerUserCommand.Email))
                .ForMember(user => user.Password, y => y.MapFrom(registerUserCommand => registerUserCommand.Password));

            CreateMap<UpdateUserCommand, User>()
                .ForMember(user => user.FirstName, y => y.MapFrom(updateUserCommand => updateUserCommand.FirstName))
                .ForMember(user => user.LastName, y => y.MapFrom(updateUserCommand => updateUserCommand.LastName))
                .ForMember(user => user.Email, y => y.MapFrom(updateUserCommand => updateUserCommand.Email))
                .ForMember(user => user.DateOfBirth, y => y.MapFrom(updateUserCommand => updateUserCommand.DateOfBirth))
                .ForMember(user => user.Username, y => y.MapFrom(updateUserCommand => updateUserCommand.Username))
                .ForMember(user => user.Password, y => y.MapFrom(registerUserCommand => registerUserCommand.Password))
                .ForMember(user => user.Role, y => y.MapFrom(updateUserCommand => updateUserCommand.Role));
        }
    }
}
