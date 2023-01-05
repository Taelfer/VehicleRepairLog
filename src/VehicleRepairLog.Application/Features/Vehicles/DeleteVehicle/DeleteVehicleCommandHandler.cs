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
    public class DeleteVehicleCommand : IRequest<VehicleDto>
    {
        public int VehicleId;
    }

    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, VehicleDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleProfileStorageContext _context;

        public DeleteVehicleCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<VehicleDto> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            Vehicle vehicle = await _context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == request.VehicleId);

            if (vehicle is null)
            {
                throw new NotFoundException("Vehicle has not been found.");
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleDto>(vehicle);
        }
    }
}
