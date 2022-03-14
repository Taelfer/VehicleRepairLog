using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Vehicles;
using VehicleRepairLog.DataAccess.Entities;

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

            if (vehicles is null)
            {
                return new GetAllVehiclesResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetAllVehiclesResponse()
            {
                Data = this.mapper.Map<List<VehicleDto>>(vehicles)
            };
        }
    }
}
