using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.Features.Users.DeleteUser
{
    public class DeleteUserCommand : IRequest<UserDto>
    {
        public int userId;
    }

    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleProfileStorageContext _context;

        public DeleteUserCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // Returns User data with given ID from DB.
            User user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.userId);

            if (user is null)
            {
                throw new NotFoundException("User has not been found.");
            }

            // Removes User of given ID from DB.
            _context.Users.Remove(user);

            // Saves changes made to DB.
            await _context.SaveChangesAsync();
            
            return _mapper.Map<UserDto>(user);
        }
    }
}
