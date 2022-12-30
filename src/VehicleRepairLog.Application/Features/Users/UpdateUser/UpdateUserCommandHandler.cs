using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.Features.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public int UserId;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public List<string> Vehicles { get; set; }
    }

    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly VehicleProfileStorageContext _context;

        public UpdateUserCommandHandler(IMapper mapper, IPasswordHasher<User> passwordHasher, VehicleProfileStorageContext context)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _context = context;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.UserId);

            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }

            User updatedUser = _mapper.Map(request, user);

            string hashedPassword = _passwordHasher.HashPassword(user, request.Password);
            updatedUser.Password = hashedPassword;

            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}