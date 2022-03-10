using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Users;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Users
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, GetAllUsersResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetAllUsersHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetAllUsersResponse> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var query = new GetAllUsersQuery() { };
            var users = await this.queryExecutor.Execute(query);

            if (users is null)
            {
                return new GetAllUsersResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetAllUsersResponse()
            {
                Data = this.mapper.Map<List<Domain.Models.UserDto>>(users)
            };
        }
    }
}
