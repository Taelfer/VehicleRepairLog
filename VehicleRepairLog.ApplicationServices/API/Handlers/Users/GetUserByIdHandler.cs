using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Users;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;
        private readonly IUserService userService;

        public GetUserByIdHandler(IMapper mapper, IQueryExecutor queryExecutor, IUserService userService)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
            this.userService = userService;
        }

        public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var claim = userService.GetCurrentUser();
            User user = null;

            var query = new GetUserByIdQuery()
            {
                Id = request.UserId
            };

            if (claim.Role == "User" || claim.Role == "Admin")
            {
                user = await this.queryExecutor.Execute(query);
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
