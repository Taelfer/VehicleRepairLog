using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Domain.Entities;
using VehicleRepairLog.Domain.Exceptions;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Users
{
    public class GetUserByIdQuery : IRequest<UserViewDto>
    {
        public int UserId;
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewDto>
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

        public async Task<UserViewDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var claim = userService.GetCurrentUser();
            User user = null;

            if (claim.Role == "Admin" || claim.Role == "User")
            {
                user = await this.context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            }
            else
            {
                throw new UnauthorizedException("You have no access to this resource.");
            }
            
            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }

            return this.mapper.Map<UserViewDto>(user);
        }
    }
}
