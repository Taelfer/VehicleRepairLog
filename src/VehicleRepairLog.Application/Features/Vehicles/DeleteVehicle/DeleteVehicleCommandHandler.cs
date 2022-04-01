using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Domain.Exceptions;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Vehicles
{
    public class DeleteVehicleCommand : IRequest<VehicleDto>
    {
        public int VehicleId;
    }

    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, VehicleDto>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public DeleteVehicleCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<VehicleDto> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await this.context.Vehicles.FirstOrDefaultAsync(x => x.Id == request.VehicleId);

            if (vehicle is null)
            {
                throw new NotFoundException("Vehicle not found.");
            }

            this.context.Vehicles.Remove(vehicle);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<VehicleDto>(vehicle);
        }
    }
}
