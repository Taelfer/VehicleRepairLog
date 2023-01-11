using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Vehicles
{
    public class GetVehicleByIdQuery : IRequest<VehicleDto>
    {
        public int VehicleId;
    }

    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleRepairLogContext _context;

        public GetVehicleByIdQueryHandler(IMapper mapper, VehicleRepairLogContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<VehicleDto> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            Vehicle vehicle = await _context.Vehicles
                            .Include(vehicle => vehicle.Repairs)
                            .FirstOrDefaultAsync(vehicle => vehicle.Id == request.VehicleId);

            if (vehicle is null)
            {
                throw new NotFoundException("Vehicle not found.");
            }

            return _mapper.Map<VehicleDto>(vehicle);
        }
    }
}
