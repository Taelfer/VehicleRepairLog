using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.Features.Vehicles
{
    public class GetAllVehiclesQuery : IRequest<List<VehicleDto>>
    {
    }

    public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, List<VehicleDto>>
    {
        private readonly IMapper _mapper;
        private readonly VehicleProfileStorageContext _context;

        public GetAllVehiclesQueryHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<VehicleDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            //if (cancellationToken is null)
            //{
            //    throw new TaskCanceledException("Task canceled");
            //}

            List<Vehicle> vehicles = await _context.Vehicles.ToListAsync(cancellationToken);

            if (vehicles is null)
            {
                throw new NotFoundException("Vehicles not found.");
            }

            return _mapper.Map<List<VehicleDto>>(vehicles);
        }
    }
}
