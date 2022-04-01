using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Domain.Entities;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Users
{
    public class RegisterUserCommand : IRequest<UserViewDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserViewDto>
    {
        private readonly IMapper mapper;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly VehicleProfileStorageContext context;

        public RegisterUserCommandHandler(IMapper mapper, IPasswordHasher<User> passwordHasher, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
            this.context = context;
        }

        public async Task<UserViewDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = this.mapper.Map<User>(request);

            var hashedPassword = this.passwordHasher.HashPassword(user, request.Password);
            user.Password = hashedPassword;

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<UserViewDto>(user);
        }
    }
}
