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
    public class UpdateVehicleCommand : IRequest<VehicleDto>
    {
        public int VehicleId;
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string VinNumber { get; set; }
        public string PaintColor { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
    }

    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, VehicleDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleProfileStorageContext _context;

        public UpdateVehicleCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<VehicleDto> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            Vehicle vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == request.VehicleId);

            if (vehicle is null)
            {
                throw new NotFoundException("Vehicle not found.");
            }

            Vehicle updatedVehicle = _mapper.Map(request, vehicle);
            _context.Vehicles.Update(updatedVehicle);
            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleDto>(vehicle);
        }
    }
}
