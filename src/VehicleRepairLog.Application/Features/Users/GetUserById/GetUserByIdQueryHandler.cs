using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Authentication;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.Features.Users
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int UserId;
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleProfileStorageContext _context;

        public GetUserByIdQueryHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.UserId);
            
            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
