using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Users.DeleteUser
{
    /// <summary>
    ///     Passes data between HTTP Requests and MediatR handler.
    /// </summary>
    public class DeleteUserCommand : IRequest<UserDto>
    {
        public int UserId;
    }

    /// <summary>
    ///     Deletes <see cref="User"/> from Database.
    /// </summary>
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleRepairLogContext _context;

        public DeleteUserCommandHandler(IMapper mapper, VehicleRepairLogContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // Returns User data with given ID from Database.
            User user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.UserId);

            if (user is null)
            {
                throw new NotFoundException("User has not been found.");
            }

            // Removes User of given ID from Database.
            _context.Users.Remove(user);

            // Saves changes made to Database.
            await _context.SaveChangesAsync();
            
            return _mapper.Map<UserDto>(user);
        }
    }
}
