using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Vehicles
{
    public class GetAllVehiclesQuery : IRequest<List<VehicleDto>>
    {
    }

    public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, List<VehicleDto>>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public GetAllVehiclesQueryHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<List<VehicleDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await this.context.Vehicles.ToListAsync();

            if (vehicles is null)
            {
                throw new NotFoundException("Vehicles not found.");
            }

            return this.mapper.Map<List<VehicleDto>>(vehicles);
        }
    }
}
