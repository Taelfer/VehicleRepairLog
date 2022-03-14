using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Parts;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class GetAllPartsHandler : IRequestHandler<GetAllPartsRequest, GetAllPartsResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;
        private readonly IUserService userClaims;

        public GetAllPartsHandler(IQueryExecutor queryExecutor, IMapper mapper, IUserService userClaims)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
            this.userClaims = userClaims;
        }

        public async Task<GetAllPartsResponse> Handle(GetAllPartsRequest request, CancellationToken cancellationToken)
        {
            var claims = userClaims.GetCurrentUser();
            List<Part> parts = null;

            var query = new GetAllPartsQuery()
            {
                Name = request.Name
            };

            if (claims.Role is "Admin")
            {
                parts = await this.queryExecutor.Execute(query);
            }
            else
            {
                return new GetAllPartsResponse()
                {
                    Error = new ErrorModel(ErrorType.Unauthorized)
                };
            }

            if (parts is null)
            {
                return new GetAllPartsResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetAllPartsResponse()
            {
                Data = this.mapper.Map<List<Domain.Models.PartDto>>(parts)
            };
        }
    }
}
