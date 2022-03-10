using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Vehicles;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Vehicles
{
    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesRequest, GetAllVehiclesResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetAllVehiclesHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetAllVehiclesResponse> Handle(GetAllVehiclesRequest request, CancellationToken cancellationToken)
        {
            var query = new GetAllVehiclesQuery() { };
            var vehicles = await this.queryExecutor.Execute(query);

            return new GetAllVehiclesResponse()
            {
                Data = this.mapper.Map<List<Domain.Models.VehicleDto>>(vehicles)
            };
        }
    }
}
