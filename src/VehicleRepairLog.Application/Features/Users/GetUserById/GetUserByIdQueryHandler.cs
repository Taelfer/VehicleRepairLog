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
        private readonly IUserService _userService;
        private readonly VehicleProfileStorageContext _context;

        public GetUserByIdQueryHandler(IMapper mapper, IUserService userService, VehicleProfileStorageContext context)
        {
            _mapper = mapper;
            _userService = userService;
            _context = context;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            UserDto claim = _userService.GetCurrentUser();
            User user = null;

            if (claim.Role == "Admin" || claim.Role == "User")
            {
                user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            }
            else
            {
                throw new UnauthorizedException("You have no access to this resource.");
            }
            
            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
