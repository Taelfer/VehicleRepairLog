using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Domain.Entities;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Vehicles
{
    public class AddVehicleCommand : IRequest<VehicleDto>
    {
        public string BrandName { get; set; }
        public string VinNumber { get; set; }
        public string PaintColor { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public int UserId { get; set; }
    }

    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, VehicleDto>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public AddVehicleCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<VehicleDto> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = this.mapper.Map<Vehicle>(request);

            this.context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            return this.mapper.Map<VehicleDto>(vehicle);
        }
    }
}
