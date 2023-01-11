using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

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
    }

    internal class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleRepairLogContext _context;

        public UpdateUserDetailsCommandHandler(IMapper mapper, VehicleRepairLogContext context)
        {
            _mapper = mapper;
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
            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}