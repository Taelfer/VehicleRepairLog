using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Parts;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class GetAllPartsHandler : IRequestHandler<GetAllPartsRequest, GetAllPartsResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetAllPartsHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetAllPartsResponse> Handle(GetAllPartsRequest request, CancellationToken cancellationToken)
        {
            var query = new GetAllPartsQuery();
            var parts = await this.queryExecutor.Execute(query);
            var mappedParts = this.mapper.Map<List<Domain.Models.Part>>(parts);

            var response = new GetAllPartsResponse()
            {
                Data = mappedParts.ToList()
            };

            return response;
        }
    }
}
