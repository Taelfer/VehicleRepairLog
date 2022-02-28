using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Repairs;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Repairs
{
    public class GetRepairByIdHandler : IRequestHandler<GetRepairByIdRequest, GetRepairByIdResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetRepairByIdHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetRepairByIdResponse> Handle(GetRepairByIdRequest request, CancellationToken cancellationToken)
        {
            var query = new GetRepairByIdQuery()
            {
                Id = request.RepairId
            };
            var repair = await this.queryExecutor.Execute(query);

            return new GetRepairByIdResponse()
            {
                Data = this.mapper.Map<Domain.Models.Repair>(repair)
            };
        }
    }
}
