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
    public class UpdateVehicleCommand : IRequest<VehicleDto>
    {
        public int VehicleId;
        public string BrandName { get; set; }
        public string VinNumber { get; set; }
        public string PaintColor { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
    }

    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, VehicleDto>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public UpdateVehicleCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<VehicleDto> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await this.context.Vehicles.FirstOrDefaultAsync(x => x.Id == request.VehicleId);

            if (vehicle is null)
            {
                throw new NotFoundException("Vehicle not found.");
            }

            var updatedVehicle = this.mapper.Map(request, vehicle);
            this.context.Vehicles.Update(updatedVehicle);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<VehicleDto>(vehicle);
        }
    }
}
