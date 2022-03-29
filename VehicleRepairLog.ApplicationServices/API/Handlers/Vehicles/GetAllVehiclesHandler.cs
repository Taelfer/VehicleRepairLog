using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly VehicleProfileStorageContext context;

        public GetAllVehiclesHandler(IMapper mapper, IQueryExecutor queryExecutor, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
            this.context = context;
        }

        public async Task<GetAllVehiclesResponse> Handle(GetAllVehiclesRequest request, CancellationToken cancellationToken)
        {
            var vehicles = await context.Vehicles.ToListAsync();

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
