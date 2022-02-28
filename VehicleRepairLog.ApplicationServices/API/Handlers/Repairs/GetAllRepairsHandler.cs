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
using VehicleRepairLog.DataAccess.Entities;

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

            return new GetAllRepairsResponse()
            {
                Data = this.mapper.Map<List<Domain.Models.Repair>>(repairs)
            };
        }
    }
}
