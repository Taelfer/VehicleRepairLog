using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Domain.Entities;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Users
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int UserId;
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly VehicleProfileStorageContext context;

        public GetUserByIdQueryHandler(IMapper mapper, IUserService userService, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.context = context;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var claim = userService.GetCurrentUser();
            User user = null;

            if (claim.Role == "User" || claim.Role == "Admin")
            {
                user = await this.context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            }
            else
            {
                return null;
            }
            
            if (user is null)
            {
                return null;
            }

            return this.mapper.Map<UserDto>(user);
        }
    }
}
