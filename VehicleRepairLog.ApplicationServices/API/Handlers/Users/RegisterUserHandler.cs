using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Commands.Users;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        private readonly IPasswordHasher<User> passwordHasher;

        public RegisterUserHandler(IMapper mapper, ICommandExecutor commandExecutor, IPasswordHasher<User> passwordHasher)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
            this.passwordHasher = passwordHasher;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            // CREATE MAPPING FROM RegisterUserQuery TO USER ENTITY
            var user = this.mapper.Map<User>(request);

            this.passwordHasher.HashPassword(user, request.Password);

            var command = new RegisterUserCommand()
            {
                Parameter = user
            };
            var commandFromDb = await this.commandExecutor.Execute(command);

            return new RegisterUserResponse()
            {
                //CREATE MAPPING FROM USER ENTITY TO USER DTO WITHOUT PASSWORD
                Data = this.mapper.Map<Domain.Models.UserDto>(commandFromDb)
            };
        }
    }
}
