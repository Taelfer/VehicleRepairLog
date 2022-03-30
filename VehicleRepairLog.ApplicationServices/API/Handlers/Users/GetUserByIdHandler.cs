using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly VehicleProfileStorageContext context;

        public GetUserByIdHandler(IMapper mapper, IUserService userService, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.context = context;
        }

        public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var claim = userService.GetCurrentUser();
            User user = null;

            if (claim.Role == "User" || claim.Role == "Admin")
            {
                user = await this.context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            }
            else
            {
                return new GetUserByIdResponse()
                {
                    Error = new ErrorModel(ErrorType.Unauthorized)
                };
            }
            
            if (user is null)
            {
                return new GetUserByIdResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetUserByIdResponse()
            {
                Data = this.mapper.Map<UserDto>(user)
            };
        }
    }
}
