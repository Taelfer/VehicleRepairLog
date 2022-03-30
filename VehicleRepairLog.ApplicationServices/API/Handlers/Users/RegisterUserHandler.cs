using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IMapper mapper;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly VehicleProfileStorageContext context;

        public RegisterUserHandler(IMapper mapper, IPasswordHasher<User> passwordHasher, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
            this.context = context;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var user = this.mapper.Map<User>(request);

            var hashedPassword = this.passwordHasher.HashPassword(user, request.Password);
            user.Password = hashedPassword;

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();

            return new RegisterUserResponse()
            {
                Data = this.mapper.Map<UserDto>(user)
            };
        }
    }
}
