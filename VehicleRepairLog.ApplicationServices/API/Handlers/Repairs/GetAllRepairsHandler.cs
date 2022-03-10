using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Repairs;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Repairs
{
    public class GetAllRepairsHandler : IRequestHandler<GetAllRepairsRequest, GetAllRepairsResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetAllRepairsHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetAllRepairsResponse> Handle(GetAllRepairsRequest request, CancellationToken cancellationToken)
        {
            var query = new GetAllRepairsQuery() { };
            var repairs = await this.queryExecutor.Execute(query);

            if (repairs is null)
            {
                return new GetAllRepairsResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetAllRepairsResponse()
            {
                Data = this.mapper.Map<List<Domain.Models.RepairDto>>(repairs)
            };
        }
    }
}
